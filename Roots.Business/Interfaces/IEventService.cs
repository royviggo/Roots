using Roots.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllAsync();
        Task<EventDto> GetByIdAsync(int id);
    }
}
