using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Mapper;
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
            var readings = _context.Scale.ToList();
            return readings;
        }
        public IEnumerable<ScaleEntity> GetScalesWithAnyReadings()
        {
            var readings = _context.Scale.Where(s => s.Readings.Any(r => r.ScaleId == s.ScaleId)).ToList();
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

        public int Create(ScaleCreateDto dto)
        {
            var entity = ScaleMapper.MapToEntity(dto);
            _context.Scale.Add(entity);
            _context.SaveChanges();
            return entity.ScaleId;

        }

        public bool Update(int scaleId, ScaleCreateDto dto)
        {
            var entity = _context.Scale.FirstOrDefault(x => x.ScaleId == scaleId);
            if (entity is null) return false;

            entity.ScaleName = dto.ScaleName;
            entity.ItemName = dto.ItemName;
            entity.SingleItemWeight = dto.SingleItemWeight;
            entity.IsConnected = dto.IsConnected;

            _context.SaveChanges();
            return true;
        }
    }
}
