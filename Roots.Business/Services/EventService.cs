using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using System.Collections.Generic;
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
