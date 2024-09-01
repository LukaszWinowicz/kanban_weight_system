using ApiServer.Core.DTOs;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingsRepository _repository;

        public ReadingsController(IReadingsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("latest")]
        public ActionResult<IEnumerable<ScaleReadingDto>> GetLatestReadingForEveryScale()
        {
            var value = _repository.GetLatestReadingForEveryScale();
            return Ok(value);
        }
    }
}


