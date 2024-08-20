using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleService
    {
        IEnumerable<ScaleEntity> GetAll();
        bool Delete(int scaleId);
        int Create(ScaleCreateDto dto);
    }
}
