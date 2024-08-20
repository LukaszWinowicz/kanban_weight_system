using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Mapper;

namespace ApiServer.Core.Services
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleRepository _repository;

        public ScaleService(IScaleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ScaleEntity> GetAll()
        {
            var readings = _repository.GetAll();
            return readings;
        }

        public bool Delete(int scaleId)
        {
            var scale = _repository.Delete(scaleId);
            return scale;
        }
        public int Create(ScaleCreateDto dto)
        {
            var scaleId = _repository.Create(dto);
            return scaleId;

        }
    }
}
