using ApiServer.Core.DTOs;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingsService _readingsService;
        private readonly Esp32DataService _esp32DataService;

        public ReadingsController(IReadingsService readingsService, Esp32DataService esp32DataService)
        {
            _readingsService = readingsService;
            _esp32DataService = esp32DataService;
        }

        [HttpGet("latest")]
        public ActionResult<IEnumerable<ScaleReadingDto>> GetLatestReadingForEveryScale()
        {
            var value = _readingsService.GetLatestReadingForEveryScale();
            return Ok(value);
        }

        [HttpGet("getByScaleName/{scaleName}")]
        public ActionResult<IEnumerable<ScaleReadingDto>> GetReadingsByScaleName(string scaleName)
        {
            var result = _readingsService.GetAllReadingsByScaleName(scaleName);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("getNewReading")]
        public ActionResult GetNewDataFromScale()
        {
            var isConnected = _esp32DataService.IsScaleConnectedAsync();
           
            if (isConnected == true)
            {
                return Ok();
            }

            return NotFound();


        }
    }
}


