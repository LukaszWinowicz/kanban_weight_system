using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using ApiServer.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingsService _service;
        private readonly ApiServerContext _context;

        public ReadingsController(IReadingsService service, ApiServerContext context)
        {
            _service = service;
            _context = context;
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

        [HttpGet("getByScaleId/{scaleId}")]
        public ActionResult<IEnumerable<ScaleReadingDto>> GetReadingsByScaleId(int scaleId)
        {
            var result = _service.GetReadingsByScaleId(scaleId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}


