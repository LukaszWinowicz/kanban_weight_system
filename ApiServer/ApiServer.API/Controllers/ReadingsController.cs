using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingsRepository _repository;
        private readonly ApiServerContext _context;

        public ReadingsController(IReadingsRepository repository, ApiServerContext context)
        {
            _repository = repository;
            _context = context;
        }
    }
}


