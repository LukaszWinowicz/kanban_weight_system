using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorReadingsController : ControllerBase
    {
        private readonly SensorReadingService _sensorReadingService;

        public SensorReadingsController(SensorReadingService _sensorReadingService)
        {
            _sensorReadingService = sensorReadingService;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<SensorReadingEntity>> GetAll()
        {           
            var readings = _sensorReadingService.SensorReadings.ToList();
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


