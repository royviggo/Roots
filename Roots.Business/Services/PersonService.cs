using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Exceptions;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using Roots.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;

        public PersonService(IRootsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var query = _context.Persons;

            return await query.ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Paged<IEnumerable<PersonDto>>> GetPagedAsync(PersonFilter filter)
        {
            var query = _context.Persons.AsQueryable();

            // query by name
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.FirstName.Contains(filter.Name) || p.LastName.Contains(filter.Name));

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<PersonDto>>
            {
                Data = await query.ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync(),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Events)
                .ThenInclude(e => e.EventType)
                .Include(p => p.Events)
                .ThenInclude(e => e.Place)
                .FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<int> Create(PersonCreateRequest request, CancellationToken cancellationToken)
        {
            var entity = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Status = request.Status,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _context.Persons.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<bool> Update(PersonUpdateRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Persons.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Gender = request.Gender;
            entity.Status = request.Status;
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return !cancellationToken.IsCancellationRequested;
        }

        public async Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Persons
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            _context.Persons.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return !cancellationToken.IsCancellationRequested;
        }
    }
}
