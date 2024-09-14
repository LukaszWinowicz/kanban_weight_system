using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;

namespace ApiServer.Core.Services
{
    public class Esp32DataService
    {
        private readonly IScaleRepository _scaleRepository;
        public Esp32DataService(IScaleRepository scaleRepository) 
        {
            _scaleRepository = scaleRepository; 
        }

        public IEnumerable<ScaleEntity> ScalesList()
        {
            var entities = _scaleRepository.GetAll();
            return entities;
        }

        public bool IsScaleConnectedAsync(string scaleName)
        {
            var scales = ScalesList();

            var scale = scales.FirstOrDefault(s => s.ScaleName.Equals(scaleName, StringComparison.OrdinalIgnoreCase));

            return scale != null && scale.IsConnected;
            
        }
    }
}
