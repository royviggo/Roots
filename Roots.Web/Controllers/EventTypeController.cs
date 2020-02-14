using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Filters;
using Roots.Business.Interfaces;
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
    }
}
