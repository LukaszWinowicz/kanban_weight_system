using ApiServer.Core.DTOs;
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

        /*public int Create(SensorReadingCreateDto dto)
        {
            var entity = SensorReadingMapper.ToEntity(dto);
            _context.SensorReadings.Add(entity);
            _context.SaveChanges();
            return entity.SensorId;
        }*/
    }
}
