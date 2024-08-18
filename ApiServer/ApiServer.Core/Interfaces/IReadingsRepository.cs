using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsRepository
    {
        IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale();
        Task<ReadingEntity> AddAsync(ReadingEntity readingEntity);
    }
}
