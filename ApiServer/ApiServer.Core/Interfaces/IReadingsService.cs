using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsService
    {
        IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale();
        Task<ReadingEntity> CreateReadingAsync(ReadingCreateDto createDto);
    }
}
