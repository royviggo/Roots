using Roots.Business.Models;
using Roots.Business.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IChildService
    {
        Task<ChildDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ChildDto> GetByPersonIdAsync(int personId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChildDto>> GetByFamilyIdAsync(int familyId, CancellationToken cancellationToken = default);
        Task<int> Create(ChildCreateRequest request, CancellationToken cancellationToken = default);
        Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}
