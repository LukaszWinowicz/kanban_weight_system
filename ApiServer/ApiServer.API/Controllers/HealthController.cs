using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok("API is running");
        }
    }
}
