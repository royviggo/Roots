using Roots.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Business.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<PersonDto> GetByIdAsync(int id);
    }
}
