using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleService
    {
        IEnumerable<ScaleEntity> GetAll();
        IEnumerable<ScaleEntity> GetScalesWithAnyReadings();
        bool Delete(int scaleId);
        int Create(ScaleCreateDto dto);
        bool Update(int scaleId, ScaleCreateDto dto);
    }
}
