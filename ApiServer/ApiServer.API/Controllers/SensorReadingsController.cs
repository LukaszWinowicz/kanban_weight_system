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

        /*[HttpGet("{sensorId}")]
        public ActionResult<SensorReadingEntity> GetById(int sensorId)
        {
            var reading = _context.SensorReadings.FirstOrDefault(x => x.SensorId == sensorId);
            return reading;
        }

        [HttpPost]
        public ActionResult<SensorReadingCreateDto> CreateSensorReading([FromBody] SensorReadingCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SensorReadings.Add(createDto);
            _context.SaveChanges();
            return Ok();*/

    }
}


