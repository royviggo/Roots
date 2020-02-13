using Roots.Business.Filters;
using Roots.Business.Models;
using Roots.Business.Requests;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Paged<IEnumerable<EventDto>>> GetPagedAsync(EventFilter filter, CancellationToken cancellationToken = default);
        Task<EventDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> Create(EventCreateRequest request, CancellationToken cancellationToken = default);
        Task<bool> Update(EventUpdateRequest request, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}
