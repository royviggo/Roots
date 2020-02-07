using Microsoft.AspNetCore.Mvc;
using Roots.Business.Interfaces;
using Roots.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roots.Web.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EventDto>> Get()
        {
            return await _eventService.GetAllAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<EventDto> Get(int id)
        {
            return await _eventService.GetByIdAsync(id);
        }
    }
}
