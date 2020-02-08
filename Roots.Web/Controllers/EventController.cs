using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roots.Business.Interfaces;
using Roots.Web.Models;
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
        /// Get a list of events with event type and place.
        /// </summary>
        /// <returns>A list of events</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var events = await _eventService.GetAllAsync();

            if (events == null)
                return BadRequest();

            return Ok(_mapper.Map<IEnumerable<EventVm>>(events));
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

            return Ok(_mapper.Map<EventVm>(evnt));
        }
    }
}
