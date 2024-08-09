using ApiServer.Core.Dtos;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;

namespace ApiServer.Core.Services
{
    public class ReadingsService : IReadingsService
    {
        private readonly IReadingsRepository _repository;

        public ReadingsService(IReadingsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ReadingEntity> GetAll()
        {
            var readings = _repository.GetAll();
            return readings;
        }

        public ReadingEntity GetById(int id)
        {
            var read = _repository.GetById(id);
            return read;
        }

        public ReadingEntity GetLatestParamByName(int scaleId)
        {
            var value = _repository.GetLatestParamByName(scaleId);
            return value;
        }

        public IEnumerable<ReadingEntity> GetLatestSensorValue()
        {
            var value = _repository.GetLatestSensorValue();
            return value;
        }

        public int Create(ReadingCreateDto dto)
        {
            var create = _repository.Create(dto);
            return create;
        }
    }
}
