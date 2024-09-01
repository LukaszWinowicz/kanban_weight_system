using ApiServer.Core.DTOs;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsRepository
    {
        IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale();
    }
}
