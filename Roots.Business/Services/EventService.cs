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
            if (filter.DateFrom != null)
                query = query.Where(p => p.EventDate == filter.DateFrom.DateString);

            if (filter.DateTo != null)
                query = query.Where(p => p.EventDate == filter.DateTo.DateString);

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

        public async Task<int> Create(EventCreateRequest request, CancellationToken cancellationToken)
        {
            var entity = new Event
            {
                EventTypeId = request.EventTypeId,
                PersonId = request.PersonId,
                PlaceId = request.PlaceId,
                EventDate = request.EventDate.DateString,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            _context.Events.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<bool> Update(EventUpdateRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            entity.EventTypeId = request.EventTypeId;
            entity.PersonId = request.PersonId;
            entity.PlaceId = request.PlaceId;
            entity.EventDate = request.EventDate.DateString;
            entity.Description = request.Description;
            entity.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return !cancellationToken.IsCancellationRequested;
        }

        public async Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            _context.Events.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return !cancellationToken.IsCancellationRequested;
        }
    }
}
