using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
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

        public IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale()
        {
            return _repository.GetLatestReadingForEveryScale();
        }

        public IEnumerable<ScaleReadingDto> GetReadingsByScaleId(int scaleId)
        {
            return _repository.GetReadingsByScaleId(scaleId);
        }

        public async Task<ReadingEntity> CreateReadingAsync(ReadingCreateDto createDto)
        {
            var reading = new ReadingEntity
            {
                ScaleId = createDto.ScaleId,
                Date = DateTime.Now,  // Date ustawiasz tutaj lub w repozytorium
                Value = new Random().Next(1, 100) // Losowa wartość
            };

            return await _repository.AddAsync(reading);
        }

    }
}
