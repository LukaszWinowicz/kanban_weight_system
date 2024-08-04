using ApiServer.Core.Entities;
using ApiServer.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.WindowsForms.Controllers
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
    }
}
