using ConferenceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers
{

    [Route("events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController (IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]

        public IActionResult GetEvents()
        {
            var events = _eventService.GetEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]

        public IActionResult GetEventById(int id)
        {

            var events = _eventService.GetEventById(id);

            if (events == null)
            {
                return NotFound();
            }
            return Ok(events);
        }

    }
}
