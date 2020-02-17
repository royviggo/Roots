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
    /// Api endpoints for Events
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of events with event type and place. Filter on date and event type, 
        /// and paging with page number and limit.
        /// </summary>
        /// <returns>A list of events</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventVm>> Get([FromQuery]EventQuery query)
        {
            var filter = _mapper.Map<EventQuery, EventFilter>(query);
            var events = await _eventService.GetPagedAsync(filter);

            if (events == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<EventVm>>>(events));
        }

        /// <summary>
        /// Get a specific event with event type and place.
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns>An event</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventVm>> Get(int id)
        {
            var evnt = await _eventService.GetByIdAsync(id);

            if (evnt == null)
                return NotFound();

            return Ok(new Response<EventVm>(_mapper.Map<EventVm>(evnt)));
        }

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="model">The Event create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatedModel>> Create([FromBody]EventCreateModel model)
        {
            var request = _mapper.Map<EventCreateModel, EventCreateRequest>(model);

            if (request == null)
                return BadRequest();

            try
            {
                var result = await _eventService.Create(request);
                return Created(nameof(Get), new CreatedModel { Id = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates an Event.
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <param name="model">The Event update model</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody]EventUpdateModel model)
        {
            var request = _mapper.Map<EventUpdateModel, EventUpdateRequest>(model);

            if (request == null || id != request.Id)
                return BadRequest();

            try
            {
                await _eventService.Update(request);
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
        /// Delete an Event.
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteRequest { Id = id };

            try
            {
                await _eventService.Delete(request);
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
