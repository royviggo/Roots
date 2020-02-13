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
        Task<IEnumerable<EventDto>> GetAllAsync();
        Task<Paged<IEnumerable<EventDto>>> GetPagedAsync(EventFilter filter);
        Task<EventDto> GetByIdAsync(int id);
        Task<int> Create(EventCreateRequest request, CancellationToken cancellationToken);
        Task<bool> Update(EventUpdateRequest request, CancellationToken cancellationToken);
        Task<bool> Delete(DeleteRequest request, CancellationToken cancellationToken);
    }
}
