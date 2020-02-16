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
        public async Task<IActionResult> Get([FromQuery]EventQuery query)
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
        public async Task<IActionResult> Get(int id)
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
        public async Task<ActionResult<int>> Create([FromBody]EventCreateModel model)
        {
            var request = _mapper.Map<EventCreateModel, EventCreateRequest>(model);

            return await _eventService.Create(request);
        }

        /// <summary>
        /// Updates an Event.
        /// </summary>
        /// <param name="model">The Event update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody]EventUpdateModel model)
        {
            var request = _mapper.Map<EventUpdateModel, EventUpdateRequest>(model);

            return await _eventService.Update(request);
        }

        /// <summary>
        /// Delete an Event.
        /// </summary>
        /// <param name="model">The Event delete model</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete]
        public async Task<ActionResult<int>> Delete([FromBody]DeleteModel model)
        {
            var request = _mapper.Map<DeleteModel, DeleteRequest>(model);

            return await _eventService.Delete(request);
        }
    }
}
