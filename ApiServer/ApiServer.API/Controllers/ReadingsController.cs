using ApiServer.Core.Dtos;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingsService _service;

        public ReadingsController(IReadingsService service)
        {
            _service = service;
        }

        [HttpGet("latest")]
        public ActionResult<IEnumerable<ReadingEntity>> GetLatestReadingForEveryScale()
        {
            var value = _service.GetLatestReadingForEveryScale();
            return Ok(value);
        }
    }
}


