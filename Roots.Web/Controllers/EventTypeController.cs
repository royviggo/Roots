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
        public async Task<IActionResult> Get([FromQuery]EventTypeQuery query)
        {
            var filter = _mapper.Map<EventTypeQuery, EventTypeFilter>(query);
            var events = await _eventTypeService.GetPagedAsync(filter);

            if (events == null)
                return BadRequest();

            return Ok(_mapper.Map<PagedResponse<IEnumerable<EventTypeVm>>>(events));
        }

        /// <summary>
        /// Get a specific eventtype.
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns>An eventtype</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var evnt = await _eventTypeService.GetByIdAsync(id);

            if (evnt == null)
                return NotFound();

            return Ok(new Response<EventTypeVm>(_mapper.Map<EventTypeVm>(evnt)));
        }

        /// <summary>
        /// Create a new EventType.
        /// </summary>
        /// <param name="model">The EventType create model</param>
        /// <returns>Inserted id</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]EventTypeCreateModel model)
        {
            var request = _mapper.Map<EventTypeCreateModel, EventTypeCreateRequest>(model);

            return await _eventTypeService.Create(request);
        }

        /// <summary>
        /// Updates a EventType.
        /// </summary>
        /// <param name="model">The EventType update model</param>
        /// <returns>Number of records updated</returns>
        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody]EventTypeUpdateModel model)
        {
            var request = _mapper.Map<EventTypeUpdateModel, EventTypeUpdateRequest>(model);

            return await _eventTypeService.Update(request);
        }

        /// <summary>
        /// Delete a EventType.
        /// </summary>
        /// <param name="model">The EventType delete model</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete]
        public async Task<ActionResult<int>> Delete([FromBody]DeleteModel model)
        {
            var request = _mapper.Map<DeleteModel, DeleteRequest>(model);

            return await _eventTypeService.Delete(request);
        }
    }
}
