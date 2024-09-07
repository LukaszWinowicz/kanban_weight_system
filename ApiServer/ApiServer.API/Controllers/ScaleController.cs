using ApiServer.Core.DTOs;
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
        public ActionResult<IEnumerable<ScaleDto>> GetAll()
        {
            var values = _service.GetAll();
            return Ok(values);
        }

        [HttpGet("withreadings")]
        public ActionResult<IEnumerable<ScaleDto>> GetScalesWithAnyReadings()
        {
            var readings = _service.GetScalesWithAnyReadings();
            return Ok(readings);
        }

        [HttpPost]
        public ActionResult CreateScale([FromBody] ScaleCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isCreated = _service.CreateScale(dto);

            if (isCreated == true)
            {
                return CreatedAtAction(nameof(CreateScale), new { isCreated = dto.ItemName }, dto);
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the scale.");
            }
        }

        [HttpDelete("delete/{scaleName}")]
        public ActionResult DeleteScale([FromRoute] string scaleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isDeleted = _service.DeleteScale(scaleName);
            if (isDeleted == true)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
