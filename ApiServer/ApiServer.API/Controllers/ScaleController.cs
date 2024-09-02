using ApiServer.Core.DTOs;
using ApiServer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaleController : ControllerBase
    {
        private readonly IScaleService _service;

        public ScaleController(IScaleService service)
        {
            _service = service;
        }

        [HttpGet("all")] // .../api/Scale/all
        public ActionResult<IEnumerable<ScaleDto>> GetAll()
        {
            var readings = _service.GetAll();
            return Ok(readings);
        }

    }
}
