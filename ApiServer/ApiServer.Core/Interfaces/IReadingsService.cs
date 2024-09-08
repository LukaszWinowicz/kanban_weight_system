using ApiServer.Core.DTOs;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsService
    {
        IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale();
        IEnumerable<ScaleReadingDto> GetAllReadingsByScaleName(string scaleName);
        bool GetNewDataFromScale(string scaleName);
    }
}
