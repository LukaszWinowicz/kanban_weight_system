using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;

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

        public IEnumerable<ScaleWithAllReadingsDto> G()
        {
            var readings = _repository.GetScaleWithAllReadings();
            return readings;
        }
    }
}
