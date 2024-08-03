using ApiServer.Database;
using ApiServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorReadingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorReadingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SensorReading>>> GetSensorReadings()
        {
            return await _context.SensorReadings
                .OrderByDescending(s => s.Date)
                .Take(100)
                .ToListAsync();
        }

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return "API is working!";
        }
    }
}
