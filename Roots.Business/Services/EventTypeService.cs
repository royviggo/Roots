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
    public class EventTypeService : IEventTypeService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;

        public EventTypeService(IRootsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.EventTypes
                .ProjectTo<EventTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Paged<IEnumerable<EventTypeDto>>> GetPagedAsync(EventTypeFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.EventTypes.AsQueryable();

            // query by family event
            if (filter.IsFamilyEvent != null)
                query = query.Where(p => p.IsFamilyEvent == filter.IsFamilyEvent);

            // query by name
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            // query by Gedcom tag
            if (!string.IsNullOrEmpty(filter.GedcomTag))
                query = query.Where(p => p.GedcomTag == filter.GedcomTag);

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<EventTypeDto>>
            {
                Data = await query.ProjectTo<EventTypeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<EventTypeDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var evnt = await _context.EventTypes
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<EventTypeDto>(evnt);
        }

        public async Task<int> Create(EventTypeCreateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new EventType
            {
                Name = request.Name,
                GedcomTag = request.GedcomTag,
                IsFamilyEvent = request.IsFamilyEvent,
            };

            _context.EventTypes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> Update(EventTypeUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.EventTypes.FindAsync(request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            entity.Name = request.Name;
            entity.GedcomTag = request.GedcomTag;
            entity.IsFamilyEvent = request.IsFamilyEvent;

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.EventTypes
                .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(entity, request.Id);

            _context.EventTypes.Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
