using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Interfaces;
using Roots.Web.Models;
using Roots.Web.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Web.Controllers
{
    /// <summary>
    /// Api endpoints for Person
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all persons.
        /// </summary>
        /// <returns>A list of Person models with events</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _personService.GetAllAsync();

            if (persons == null)
                return BadRequest();

            return Ok(new PagedResponse<IEnumerable<PersonVm>>(_mapper.Map<IEnumerable<PersonVm>>(persons)));
        }

        /// <summary>
        /// Gets a specific person.
        /// </summary>
        /// <param name="id">The Person Id</param>
        /// <returns>A Person model with events</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
                return BadRequest();

            return Ok(new Response<PersonVm>(_mapper.Map<PersonVm>(person)));
        }
    }
}
