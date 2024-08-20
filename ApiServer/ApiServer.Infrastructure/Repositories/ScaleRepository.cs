using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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
            var readings = _context.Scale.ToList();
            return readings;
        }

        public bool Delete(int scaleId)
        {
            var scale = _context.Scale.FirstOrDefault(x => x.ScaleId == scaleId);

            if (scale is null) return false;

            _context.Scale.Remove(scale);
            _context.SaveChanges();
            return true;
        }
    }
}
