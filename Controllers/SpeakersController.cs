using ConferenceManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers
{
    [Route("speakers")]
    [ApiController]
    public class SpeakersController : Controller
    {


        private readonly IEventService _eventService;

        public SpeakersController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetSpeakers(int eventId)
        {
            var speakers = _eventService.GetSpeakers(eventId);
            return Ok(speakers);
        }
    }
}

