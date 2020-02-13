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

        public async Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Persons;

            return await query.ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        public async Task<Paged<IEnumerable<PersonDto>>> GetPagedAsync(PersonFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Persons.AsQueryable();

            // query by name
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.FirstName.Contains(filter.Name) || p.LastName.Contains(filter.Name));

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<PersonDto>>
            {
                Data = await query.ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<PersonDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var person = await _context.Persons
                .Include(p => p.Events)
                .ThenInclude(e => e.EventType)
                .Include(p => p.Events)
                .ThenInclude(e => e.Place)
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<int> Create(PersonCreateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Status = request.Status,
            };

            _context.Persons.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> Update(PersonUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Persons.FindAsync(request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Gender = request.Gender;
            entity.Status = request.Status;

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Persons
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            _context.Persons.Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
