using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Exceptions;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Services
{
    public class ChildService : IChildService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public ChildService(IRootsDbContext context, IMapper mapper, IPersonService personService)
        {
            _context = context;
            _mapper = mapper;
            _personService = personService;
        }

        public async Task<ChildDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var evnt = await _context.Children
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<ChildDto>(evnt);
        }

        public async Task<ChildDto> GetByPersonIdAsync(int personId, CancellationToken cancellationToken = default)
        {
            var evnt = await _context.Children
                .SingleOrDefaultAsync(e => e.PersonId == personId, cancellationToken);

            return _mapper.Map<ChildDto>(evnt);
        }

        public async Task<IEnumerable<ChildDto>> GetByFamilyIdAsync(int familyId, CancellationToken cancellationToken = default)
        {
            return await _context.Children
                .Where(c => c.FamilyId == familyId)
                .ProjectTo<ChildDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> Create(ChildCreateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new Child
            {
                FamilyId = request.FamilyId,
                PersonId = request.PersonId,
            };

            _context.Children.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Children
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException();

            _context.Children.Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
