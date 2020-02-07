using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Interfaces;
using Roots.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Web.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _personService.GetAllAsync();

            if (persons == null)
                return BadRequest();

            return Ok(_mapper.Map<IEnumerable<PersonVm>>(persons));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
                return BadRequest();

            return Ok(_mapper.Map<PersonVm>(person));
        }
    }
}
