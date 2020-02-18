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

        public async Task<IEnumerable<PlaceDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context
                .Places.ProjectTo<PlaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Paged<IEnumerable<PlaceDto>>> GetPagedAsync(PlaceFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Places.AsQueryable();

            // query by name
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<PlaceDto>>
            {
                Data = await query.ProjectTo<PlaceDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<PlaceDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var evnt = await _context.Places
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<PlaceDto>(evnt);
        }

        public async Task<int> Create(PlaceCreateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new Place
            {
                Name = request.Name,
            };

            _context.Places.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> Update(PlaceUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Places.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException();

            entity.Name = request.Name;

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Places
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException();

            _context.Places.Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
