using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers
{
    [Route("/health")]
    [ApiController]

    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Server is responding");
        }
    }
}
