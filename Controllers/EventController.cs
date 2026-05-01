using ConferenceManager.Models;
using ConferenceManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ConferenceManager.Controllers
{

    [Route("events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
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


        [HttpGet("attendees/{attendeeId}")]
        [Authorize]
        public IActionResult GetAttendeeById(int attendeeId)
        {
            var currentUserId = User.FindFirst("sub")?.Value
                            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId)) return Unauthorized();


            var attendee = _eventService.GetAttendeeById(attendeeId);

            if (attendee == null)
            {
                return NotFound($"No attendee record found with ID {attendeeId}");
            }

            if(currentUserId !.Equals(attendeeId))
            {
                return Forbid();
            }

            return Ok(attendee);
        }

        [HttpPost("{eventId}/attendees")]
        [Authorize]
        public IActionResult AddAttendee(int eventId, [FromBody] Attendee attendee)
        {
            // 1. validate input
            if (attendee == null || string.IsNullOrEmpty(attendee.Name))
                return BadRequest("Invalid attendee data");

            // 2. get user id from JWT
            var userId = User.FindFirst("sub")?.Value
                             ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var success = _eventService.AddAttendee(eventId, userId, attendee);

            if (!success)
            {
                return Conflict("Could not add attendee.");


            }

            return Ok(new
            {
                Message = "Registration successful",
                AttendeeId = attendee.Id
            });


        }




    }
}
