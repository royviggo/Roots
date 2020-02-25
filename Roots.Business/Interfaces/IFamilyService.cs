using Roots.Business.Filters;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IFamilyService
    {
        Task<IEnumerable<FamilyDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Paged<IEnumerable<FamilyDto>>> GetPagedAsync(FamilyFilter filter, CancellationToken cancellationToken = default);
        Task<FamilyDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> Create(FamilyCreateRequest request, CancellationToken cancellationToken = default);
        Task<int> Update(FamilyUpdateRequest request, CancellationToken cancellationToken = default);
        Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}
