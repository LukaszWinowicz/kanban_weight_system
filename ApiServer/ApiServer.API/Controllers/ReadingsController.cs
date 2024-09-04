using ApiServer.Core.DTOs;
using ApiServer.Core.Interfaces;
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
        public ActionResult<IEnumerable<ScaleReadingDto>> GetLatestReadingForEveryScale()
        {
            var value = _service.GetLatestReadingForEveryScale();
            return Ok(value);
        }

        [HttpGet("getByScaleName/{scaleName}")]
        public ActionResult<IEnumerable<ScaleReadingDto>> GetReadingsByScaleName(string scaleName)
        {
            var result = _service.GetAllReadingsByScaleName(scaleName);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}


