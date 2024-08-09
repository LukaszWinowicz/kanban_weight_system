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
            var readings = _context.ScaleEntities.ToList();
            return readings;
        }
    }
}
