using ApiServer.Core.DTOs;
using ApiServer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaleReadingsController : ControllerBase
    {
        private readonly IScaleService _scaleService;

        public ScaleReadingsController(IScaleService scaleService)
        {
            _scaleService = scaleService;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<ScaleWithAllReadingsDto>> GetScaleWithAllReadings()
        {
            var result = _scaleService.G();
            return Ok(result);
        }
    }
}
