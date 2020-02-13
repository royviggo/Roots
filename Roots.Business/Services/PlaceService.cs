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
    public class PlaceService : IPlaceService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;

        public PlaceService(IRootsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlaceDto>> GetAllAsync()
        {
            return await _context.Places.ProjectTo<PlaceDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Paged<IEnumerable<PlaceDto>>> GetPagedAsync(PlaceFilter filter)
        {
            var query = _context.Places.AsQueryable();

            // query by name
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<PlaceDto>>
            {
                Data = await query.ProjectTo<PlaceDto>(_mapper.ConfigurationProvider).ToListAsync(),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<PlaceDto> GetByIdAsync(int id)
        {
            var evnt = await _context.Places
                .FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<PlaceDto>(evnt);
        }

        public async Task<int> Create(PlaceCreateRequest request, CancellationToken cancellationToken)
        {
            var entity = new Place
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _context.Places.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<bool> Update(PlaceUpdateRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Places.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Place), request.Id);

            entity.Name = request.Name;
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return !cancellationToken.IsCancellationRequested;
        }

        public async Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Places
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Place), request.Id);

            _context.Places.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return !cancellationToken.IsCancellationRequested;
        }
    }
}
