using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public ActionResult<IEnumerable<ScaleReadingDto>> GetLatestReadingForEveryScale()
        {
            var value = _service.GetLatestReadingForEveryScale();
            return Ok(value);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReading([FromBody] ReadingCreateDto createDto)
        {
            await _service.CreateReadingAsync(createDto);
            return Ok();
        }
    }
}


