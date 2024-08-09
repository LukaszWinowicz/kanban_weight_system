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

        [HttpGet("test")]
        public ActionResult<IEnumerable<ReadingEntity>> GetAll()
        {
            var readings = _service.GetAll();
            return Ok(readings);
        }

        [HttpGet("id/{sensorId}")]
        public ActionResult<ReadingEntity> GetById(int sensorId)
        {
            var reading = _service.GetById(sensorId);
            return reading;
        }

        [HttpGet("name/{scaleId}")]
        public ActionResult<ReadingEntity> GetByName(int scaleId) 
        {
            var value = _service.GetLatestParamByName(scaleId);
            return Ok(value);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadingEntity>> GetLatestSensorValue()
        {
            var value = _service.GetLatestSensorValue();
            return Ok(value);
        }

        [HttpPost]
        public ActionResult<ReadingCreateDto> CreateSensorReading([FromBody] ReadingCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _service.Create(createDto);
            return Ok();
        }
    }
}


