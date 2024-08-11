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
            var readings = _context.ScaleEntities.ToList();
            return readings;
        }

        public IEnumerable<ScaleWithAllReadingsDto> GetScaleWithAllReadings()
        {
            // Include -> używane do eager loading w EF
            var query = _context.ScaleEntities
                                .Include(s => s.Readings)
                                .Select(s => new ScaleWithAllReadingsDto
                                {
                                    ScaleId = s.ScaleId,
                                    ScaleName = s.ScaleName,
                                    ItemName = s.ItemName,
                                    SingleItemWeight = s.SingleItemWeight,
                                    IsConnected = s.IsConnected,
                                    Readings = s.Readings
                                                .OrderByDescending(r => r.Date)
                                                .Select(r => new ReadingDto
                                                {
                                                    ReadId = r.ReadId,
                                                    Date = r.Date,
                                                    Value = r.Value
                                                })
                                                .ToList()
                                })
                                .ToList();

            return query;
        }

        

    }
}
