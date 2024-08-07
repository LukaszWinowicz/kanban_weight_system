using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Mapper;
using ApiServer.Core.Services;
using ApiServer.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Infrastructure.Repositories
{
    public class SensorReadingRepository : ISensorReadingRepository
    {
        private readonly ApiServerContext _context;

        public SensorReadingRepository(ApiServerContext context)
        {
            _context = context;
        }

        /*public int Create(SensorReadingCreateDto dto)
        {
            var entity = SensorReadingMapper.ToEntity(dto);
            _context.SensorReadings.Add(entity);
            _context.SaveChanges();
            return entity.SensorId;
        }*/

        public IEnumerable<SensorReadingEntity> GetAll()
        {
            var readings = _context.SensorReadings.ToList();
            return readings;
        }
    }
}
