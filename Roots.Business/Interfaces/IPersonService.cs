using Roots.Business.Filters;
using Roots.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllAsync(PersonFilter personQuery = null);
        Task<PersonDto> GetByIdAsync(int id);
    }
}
