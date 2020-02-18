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
    /// Api endpoints for EventTypes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;
        private readonly IMapper _mapper;

        public EventTypeController(IEventTypeService eventTypeService, IMapper mapper)
        {
            _eventTypeService = eventTypeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of eventtypes. Filter on name, Gedcomtag and family event. 
        /// Paging with page number and limit.
        /// </summary>
        /// <returns>A list of event types</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventTypeModel>> Get([FromQuery]EventTypeQuery query)
        {
            var filter = _mapper.Map<EventTypeQuery, EventTypeFilter>(query);
            var events = await _eventTypeService.GetPagedAsync(filter);

            if (events == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<EventTypeModel>>>(events));
        }

        /// <summary>
        /// Get a specific eventtype.
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns>An eventtype</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventTypeModel>> Get(int id)
        {
            var evnt = await _eventTypeService.GetByIdAsync(id);

            if (evnt == null)
                return NotFound();

            return Ok(new Response<EventTypeModel>(_mapper.Map<EventTypeModel>(evnt)));
        }

        /// <summary>
        /// Create a new EventType.
        /// </summary>
        /// <param name="model">The EventType create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatedModel>> Create([FromBody]EventTypeCreateModel model)
        {
            var request = _mapper.Map<EventTypeCreateModel, EventTypeCreateRequest>(model);

            if (request == null)
                return BadRequest();

            try
            {
                var id = await _eventTypeService.Create(request);
                return Created(nameof(Get), new CreatedModel { Id = id });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a EventType.
        /// </summary>
        /// <param name="id">Event Type Id</param>
        /// <param name="model">The EventType update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody]EventTypeUpdateModel model)
        {
            var request = _mapper.Map<EventTypeUpdateModel, EventTypeUpdateRequest>(model);

            if (request == null || id != request.Id)
                return BadRequest();

            try
            {
                await _eventTypeService.Update(request);
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
        /// Delete a EventType.
        /// </summary>
        /// <param name="id">Event Type Id</param>
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
                await _eventTypeService.Delete(request);
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
