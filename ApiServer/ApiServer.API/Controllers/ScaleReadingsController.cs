using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaleReadingsController : ControllerBase
    {
        private readonly IScaleService _scaleService;
        private readonly ApiServerContext _context;

        public ScaleReadingsController(IScaleService scaleService, ApiServerContext context)
        {
            _scaleService = scaleService;
            _context = context;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<ScaleWithAllReadingsDto>> GetScaleWithAllReadings()
        {
            var result = _scaleService.G();
            return Ok(result);
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<ReadingEntity>> GetLatestValueForScale()
        {
            var result = _context.ScaleEntities
                                 .Select(s => new
                                 {
                                     ScaleId = s.ScaleId,
                                     ScaleName = s.ScaleName,
                                     ItemName = s.ItemName,
                                     SingleItemWeight = s.SingleItemWeight,
                                     IsConnected = s.IsConnected,
                                     LatestReading = s.Readings
                                                     .OrderByDescending(s => s.Date)
                                                     .Select(r => new ReadingEntity
                                                     {
                                                         ReadId = r.ReadId,
                                                         Date = r.Date,
                                                         Value = r.Value,
                                                     })
                                                     .FirstOrDefault()

                                 })
                                 .ToList();
            return Ok(result);
        }

    }
}
