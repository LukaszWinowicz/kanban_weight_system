using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleRepository
    {
        IEnumerable<ScaleEntity> GetAll();
        bool Delete(int scaleId);
        int Create(ScaleCreateDto dto);
        bool Update(int scaleId, ScaleCreateDto dto);
    }
}
