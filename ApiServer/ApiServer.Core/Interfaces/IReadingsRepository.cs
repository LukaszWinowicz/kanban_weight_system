using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsRepository
    {
        IEnumerable<ReadingEntity> GetAll();
        ReadingEntity GetById(int id);
        ReadingEntity GetLatestParamByName(int scaleId);
        IEnumerable<ReadingEntity> GetLatestSensorValue();
       // int Create(SensorReadingCreateDto dto);
    }
}
