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
        Task<IEnumerable<PlaceDto>> GetAllAsync();
        Task<PlaceDto> GetByIdAsync(int id);
        Task<Paged<IEnumerable<PlaceDto>>> GetPagedAsync(PlaceFilter filter);
        Task<int> Create(PlaceCreateRequest request, CancellationToken cancellationToken);
        Task<bool> Update(PlaceUpdateRequest request, CancellationToken cancellationToken);
        Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken);
    }
}
