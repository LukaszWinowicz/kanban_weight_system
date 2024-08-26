using ApiServer.Core.Dtos;
using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsService
    {
        IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale();
        IEnumerable<ScaleReadingDto> GetReadingsByScaleId(int scaleId);
        Task<ReadingEntity> CreateReadingAsync(ReadingCreateDto createDto);
    }
}
