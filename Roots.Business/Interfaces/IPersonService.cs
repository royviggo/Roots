using Roots.Business.Filters;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Paged<IEnumerable<PersonDto>>> GetPagedAsync(PersonFilter filter, CancellationToken cancellationToken = default);
        Task<PersonExtendedDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> Create(PersonCreateRequest request, CancellationToken cancellationToken = default);
        Task<int> Update(PersonUpdateRequest request, CancellationToken cancellationToken = default);
        Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}
