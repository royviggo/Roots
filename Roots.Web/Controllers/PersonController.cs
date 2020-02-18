using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Exceptions;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
using Roots.Business.Requests;
using Roots.Business.Responses;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonModel>> Get([FromQuery]PersonQuery query)
        {
            var filter = _mapper.Map<PersonQuery, PersonFilter>(query);
            var persons = await _personService.GetPagedAsync(filter);

            if (persons.Data == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<PersonModel>>>(persons));
        }

        /// <summary>
        /// Gets a specific person.
        /// </summary>
        /// <param name="id">The Person Id</param>
        /// <returns>A Person model with events</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonModel>> Get(int id)
        {
            var person = await _personService.GetByIdAsync(id);

            if (person == null)
                return NotFound();

            return Ok(new Response<PersonModel>(_mapper.Map<PersonModel>(person)));
        }

        /// <summary>
        /// Create a new person.
        /// </summary>
        /// <param name="model">The Person create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatedModel>> Create([FromBody]PersonCreateModel model)
        {
            var request = _mapper.Map<PersonCreateModel, PersonCreateRequest>(model);

            if (request == null)
                return BadRequest();

            try
            {
                var id = await _personService.Create(request);
                return Created(nameof(Get), new CreatedModel { Id = id });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a person.
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <param name="model">The Person update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody]PersonUpdateModel model)
        {
            var request = _mapper.Map<PersonUpdateModel, PersonUpdateRequest>(model);

            if (request == null || id != request.Id)
                return BadRequest();

            try
            {
                await _personService.Update(request);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete a person.
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteRequest { Id = id };

            try
            {
                await _personService.Delete(request);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
