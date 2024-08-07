using ApiServer.Core.Interfaces;

namespace ApiServer.Core.Services
{
    public class SensorReadingService : ISensorReadingService
    {
        private readonly ISensorReadingRepository _repository;

        public SensorReadingService(ISensorReadingRepository repository)
        {
            _repository = repository;
        }
    }
}
