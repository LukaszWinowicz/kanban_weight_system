using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Mapper;
using ApiServer.Infrastructure.Database;

namespace ApiServer.Infrastructure.Repositories
{
    public class SensorReadingRepository : ISensorReadingRepository
    {
        private readonly ApiServerContext _context;

        public SensorReadingRepository(ApiServerContext context)
        {
            _context = context;
        }

        public IEnumerable<SensorReadingEntity> GetAll()
        {
            var readings = _context.SensorReadings.ToList();
            return readings;
        }

        public SensorReadingEntity GetById(int id)
        { 
            var read = _context.SensorReadings.Find(id);
            return read;
        }

        public SensorReadingEntity GetLatestParamByName(string espName)
        {
            var value = _context.SensorReadings
                .Where(x => x.EspName == espName)
                .OrderByDescending(t => t.Date)
                .FirstOrDefault();
            return value;
        }

        public IEnumerable<SensorReadingEntity> GetLatestSensorValue()
        {
            var value = _context.SensorReadings
                            .GroupBy(s => s.EspId)
                            .Select(g => g.OrderByDescending(d => d.Date).First())
                            .ToList();
            return value;
        }

        public int Create(SensorReadingCreateDto dto)
        {
            var entity = SensorReadingMapper.ToEntity(dto);
            _context.SensorReadings.Add(entity);
            _context.SaveChanges();
            return entity.SensorId;
        }
    }
}
