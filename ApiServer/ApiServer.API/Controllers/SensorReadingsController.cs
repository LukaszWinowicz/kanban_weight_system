using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorReadingsController : ControllerBase
    {
        private readonly ISensorReadingRepository _service;

        public SensorReadingsController(ISensorReadingRepository service)
        {
            _service = service;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<SensorReadingEntity>> GetAll()
        {
            var readings = _service.GetAll();
            return Ok(readings);
        }

        [HttpGet("id/{sensorId}")]
        public ActionResult<SensorReadingEntity> GetById(int sensorId)
        {
            var reading = _service.GetById(sensorId);
            return reading;
        }

        [HttpGet("name/{espName}")]
        public ActionResult<SensorReadingEntity> GetByName(string espName) 
        {
            var value = _service.GetLatestParamByName(espName);
            return Ok(value);
        }

        [HttpPost]
        public ActionResult<SensorReadingCreateDto> CreateSensorReading([FromBody] SensorReadingCreateDto createDto)
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


