using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;

namespace ApiServer.Infrastructure.Repositories
{
    public class ReadingsRepository : IReadingsRepository
    {
        private readonly ApiServerContext _context;

        public ReadingsRepository(ApiServerContext context)
        {
            _context = context;
        }

        public IEnumerable<ReadingEntity> GetAll()
        {
            var values = _context.Reading.ToList();
            return values;
        }
    }
}
