using ApiServer.Core.DTOs;
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

    }
}
