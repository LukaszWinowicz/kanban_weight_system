using ApiServer.Core.Entities;
using ApiServer.Core.Models;
using ApiServer.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorReadingsController : ControllerBase
    {
        private readonly ApiServerContext _context;

        public SensorReadingsController(ApiServerContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<SensorReading>> GetAll()
        {           
            var readings = _context.SensorReadings.ToList();
            return Ok(readings);
        }

        [HttpGet("{sensorId}")]
        public ActionResult<SensorReading> GetById(int sensorId)
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
            return Ok();

        }
    }
}
