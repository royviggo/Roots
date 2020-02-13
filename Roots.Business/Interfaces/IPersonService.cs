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
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<Paged<IEnumerable<PersonDto>>> GetPagedAsync(PersonFilter filter);
        Task<PersonDto> GetByIdAsync(int id);
        Task<int> Create(PersonCreateRequest request, CancellationToken cancellationToken);
        Task<bool> Update(PersonUpdateRequest request, CancellationToken cancellationToken);
        Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken);
    }
}
