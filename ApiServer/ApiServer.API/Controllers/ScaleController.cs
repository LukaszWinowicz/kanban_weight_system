using ApiServer.Core.Entities;
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

        [HttpGet("all")]
        public ActionResult<IEnumerable<ScaleEntity>> GetAll()
        {
            var readings = _service.GetAll();
            return Ok(readings);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        { 
            var isDeleted = _service.Delete(id);
            if (isDeleted == true)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
