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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<IEnumerable<EventDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Paged<IEnumerable<EventDto>>> GetPagedAsync(EventFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Events.AsQueryable();

            // query by date
            if (filter.DateFrom != null)
                query = query.Where(p => p.EventDate >= filter.DateFrom.DateLong);

            if (filter.DateTo != null)
                query = query.Where(p => p.EventDate <= filter.DateTo.DateLong);

            // query by eventtype
            if (filter.EventType != null)
                query = query.Where(p => p.EventTypeId == filter.EventType);

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<EventDto>>
            {
                Data = await query.ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<EventDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var evnt = await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Place)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<EventDto>(evnt);
        }

        public async Task<int> Create(EventCreateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new Event
            {
                EventTypeId = request.EventTypeId,
                PersonId = request.PersonId,
                PlaceId = request.PlaceId,
                EventDate = request.EventDate.DateLong,
                Description = request.Description,
            };

            _context.Events.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> Update(EventUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Events.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException();

            entity.EventTypeId = request.EventTypeId;
            entity.PersonId = request.PersonId;
            entity.PlaceId = request.PlaceId;
            entity.EventDate = request.EventDate.DateLong;
            entity.Description = request.Description;

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Events.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException();

            _context.Events.Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
