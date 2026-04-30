using ConferenceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers
{
    [Route("attendees")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {

        private readonly IEventService _eventService;

        public AttendeesController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAttendees([FromQuery] int eventId)
        {
            var attendees = _eventService.GetAttendees(eventId);
            if (attendees == null)
                return NotFound();
            return Ok(attendees);
        }
    }
}
