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
        public ActionResult<IEnumerable<ScaleEntity>> GetAll()
        {
            var readings = _service.GetAll();
            return Ok(readings);
        }

        [HttpGet("withreadings")]
        public ActionResult<IEnumerable<ScaleEntity>> GetScalesWithAnyReadings()
        {
            var readings = _service.GetScalesWithAnyReadings();
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

        [HttpPost]
        public ActionResult Create([FromBody] ScaleCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _service.Create(dto);

            return Ok(id);
            //return Created($"/api/.../{id}", null);
        }

        [HttpPut("{scaleId}")]
        public ActionResult Update([FromBody] ScaleCreateDto dto, [FromRoute] int scaleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _service.Update(scaleId, dto);

            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
