using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScaleController : ControllerBase
    {
        private readonly IScaleRepository _repository;

        public ScaleController(IScaleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("all")] // .../api/Scale/all
        public ActionResult<IEnumerable<ScaleEntity>> GetAll()
        {
            var readings = _repository.GetAll();
            return Ok(readings);
        }

    }
}
