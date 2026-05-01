using ConferenceManager.Models;
using ConferenceManager.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize]

        public IActionResult AddEvent([FromBody] Event newEvent)
        {
            if (newEvent == null) return BadRequest();

             _eventService.AddEvent(newEvent);

            return Ok(newEvent);

        }

        [HttpGet("{eventId}/speakers")]
        public IActionResult GetSpeakers(int eventId)
        {
            var speakers = _eventService.GetSpeakers(eventId);
            return Ok(speakers);
        }

        [HttpGet("{eventId}/attendees")]

        public IActionResult GetAttendees(int eventId)
        {
            var attendees = _eventService.GetAttendees(eventId);
            return Ok(attendees);
        }



    }
}
