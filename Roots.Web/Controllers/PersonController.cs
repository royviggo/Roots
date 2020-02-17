using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Requests;
using Roots.Web.InputModels;
using Roots.Web.Models;
using Roots.Web.Queries;
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
        /// Gets a list of all persons. Filter on name, gender and living status, and paging with page number and limit.
        /// </summary>
        /// <returns>A list of Person models with events</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PersonQuery query)
        {
            var filter = _mapper.Map<PersonQuery, PersonFilter>(query);
            var persons = await _personService.GetPagedAsync(filter);

            if (persons.Data == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<PersonVm>>>(persons));
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

        /// <summary>
        /// Create a new person.
        /// </summary>
        /// <param name="model">The Person create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]PersonCreateModel model)
        {
            var request = _mapper.Map<PersonCreateModel, PersonCreateRequest>(model);

            return await _personService.Create(request);
        }

        /// <summary>
        /// Updates a person.
        /// </summary>
        /// <param name="model">The Person update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody]PersonUpdateModel model)
        {
            var request = _mapper.Map<PersonUpdateModel, PersonUpdateRequest>(model);

            return await _personService.Update(request);
        }

        /// <summary>
        /// Delete a person.
        /// </summary>
        /// <param name="model">The Person delete model</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete]
        public async Task<ActionResult<int>> Delete([FromBody]DeleteModel model)
        {
            var request = _mapper.Map<DeleteModel, DeleteRequest>(model);

            return await _personService.Delete(request);
        }
    }
}
