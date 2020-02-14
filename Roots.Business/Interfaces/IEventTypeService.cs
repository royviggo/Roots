using Roots.Business.Filters;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IEventTypeService
    {
        Task<IEnumerable<EventTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Paged<IEnumerable<EventTypeDto>>> GetPagedAsync(EventTypeFilter filter, CancellationToken cancellationToken = default);
        Task<EventTypeDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> Create(EventTypeCreateRequest request, CancellationToken cancellationToken = default);
        Task<int> Update(EventTypeUpdateRequest request, CancellationToken cancellationToken = default);
        Task<int> Delete(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}
