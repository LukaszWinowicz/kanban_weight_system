using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsRepository
    {
        IEnumerable<ReadingEntity> GetAll();
        ReadingEntity AddReading(ReadingEntity readingEntity);
        void AddReadingsBatch(IEnumerable<ReadingEntity> readings);
    }
}
