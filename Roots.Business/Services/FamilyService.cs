using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Exceptions;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using Roots.Domain.Enums;
using Roots.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly IChildService _childService;

        public FamilyService(IRootsDbContext context, IMapper mapper, IPersonService personService, IChildService childService)
        {
            _context = context;
            _mapper = mapper;
            _personService = personService;
            _childService = childService;
        }

        public async Task<IEnumerable<FamilyDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Families
                .ProjectTo<FamilyDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Paged<IEnumerable<FamilyDto>>> GetPagedAsync(FamilyFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Families.AsQueryable();

            // paging
            query = query.Skip(filter.Skip()).Take(filter.Take());

            return new Paged<IEnumerable<FamilyDto>>
            {
                Data = await query.ProjectTo<FamilyDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
            };
        }

        public async Task<FamilyDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var evnt = await _context.Families
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            return _mapper.Map<FamilyDto>(evnt);
        }

        public async Task<int> Create(FamilyCreateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new Family();

            _context.Families.Add(entity);

            if (request.FatherId != null)
                CreatePartner((int)request.FatherId, entity.Id, PartnerRoleType.Father);

            if (request.MotherId != null)
                CreatePartner((int)request.MotherId, entity.Id, PartnerRoleType.Mother);

            foreach (var childId in request.Children)
            {
                await _childService.Create(new ChildCreateRequest
                {
                    FamilyId = entity.Id,
                    PersonId = childId,
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<int> Update(FamilyUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Families.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException();

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Families
                .Where(entity => entity.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException();

            _context.Families.Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        private void CreatePartner(int personId, int familyId, PartnerRoleType partnerRoleType)
        {
            var person = _personService.GetByIdAsync(personId);
            if (person != null)
            {
                var partner = new Partner
                {
                    FamilyId = familyId,
                    PersonId = person.Id,
                    PartnerRoleId = (int)partnerRoleType,
                };
                _context.Partners.Add(partner);
            }
        }
    }
}
