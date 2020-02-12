using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using Roots.Business.Responses;
using Roots.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roots.Business.Services
{
    public class EventService : IEventService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;

        public EventService(IRootsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            return await _context.Events.ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<Paged<IEnumerable<EventDto>>> GetPagedAsync(EventFilter filter)
        {
            var query = _context.Events.AsQueryable();

            // query by date
            if (!string.IsNullOrEmpty(filter.DateFrom))
                query = query.Where(p => p.EventDate == filter.DateFrom);

            if (!string.IsNullOrEmpty(filter.DateTo))
                query = query.Where(p => p.EventDate == filter.DateTo);

            // query by eventtype
            if (filter.EventType != null)
                query = query.Where(p => p.EventTypeId == filter.EventType);

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<EventDto>>
            {
                Data = await query.ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToListAsync(),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var evnt = await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Place)
                .FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<EventDto>(evnt);
        }

    }
}
