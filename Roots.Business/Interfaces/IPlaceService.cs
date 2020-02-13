using Roots.Business.Filters;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IPlaceService
    {
        Task<IEnumerable<PlaceDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PlaceDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Paged<IEnumerable<PlaceDto>>> GetPagedAsync(PlaceFilter filter, CancellationToken cancellationToken = default);
        Task<int> Create(PlaceCreateRequest request, CancellationToken cancellationToken = default);
        Task<int> Update(PlaceUpdateRequest request, CancellationToken cancellationToken = default);
        Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}
