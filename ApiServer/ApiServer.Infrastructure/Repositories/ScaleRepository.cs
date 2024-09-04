using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;

namespace ApiServer.Infrastructure.Repositories
{
    public class ScaleRepository : IScaleRepository
    {
        private readonly ApiServerContext _context;

        public ScaleRepository(ApiServerContext context)
        {
            _context = context;
        }

        public IEnumerable<ScaleEntity> GetAll()
        {
            var values = _context.Scale.ToList();
            return values;
        }

        public IEnumerable<ScaleEntity> GetScalesWithAnyReadings()
        {
            var values = _context.Scale.Where(s => s.Readings.Any(r => r.ScaleName == s.ScaleName)).ToList();
            return values;
        }
    }
}
