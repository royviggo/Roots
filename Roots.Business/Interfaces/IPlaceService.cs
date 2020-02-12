using Roots.Business.Filters;
using Roots.Business.Models;
using Roots.Business.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IPlaceService
    {
        Task<IEnumerable<PlaceDto>> GetAllAsync();
        Task<PlaceDto> GetByIdAsync(int id);
        Task<Paged<IEnumerable<PlaceDto>>> GetPagedAsync(PlaceFilter filter);
    }
}
