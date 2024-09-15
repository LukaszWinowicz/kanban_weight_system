using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleRepository
    {
        IEnumerable<ScaleEntity> GetAll();
        IEnumerable<ScaleEntity> GetScalesWithAnyReadings();
        bool CreateScale(ScaleEntity scale);
        bool DeleteScale(string scaleName);
    }
}
