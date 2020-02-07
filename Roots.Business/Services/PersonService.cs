using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IRootsDbContext _context;
        private readonly IMapper _mapper;

        public PersonService(IRootsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            return await _context.Persons.ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Events)
                .ThenInclude(e => e.EventType)
                .Include(p => p.Events)
                .ThenInclude(e => e.Place)
                .FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<PersonDto>(person);
        }

    }
}
