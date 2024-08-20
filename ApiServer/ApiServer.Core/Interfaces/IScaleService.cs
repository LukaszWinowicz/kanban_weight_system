using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleService
    {
        IEnumerable<ScaleEntity> GetAll();
        bool Delete(int scaleId);
    }
}
