using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Linq;
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
    }
}
