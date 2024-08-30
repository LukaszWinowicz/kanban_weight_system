using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;

namespace ApiServer.Infrastructure.Repositories
{
    public class ReadingsRepository : IReadingsRepository
    {
        private readonly ApiServerContext _context;

        public ReadingsRepository(ApiServerContext context)
        {
            _context = context;
        }

        public IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale()
        {
            var result = _context.Scale
                                 .Select(s => new ScaleReadingDto
                                 {
                                     ScaleId = s.ScaleId,
                                     ScaleName = s.ScaleName,
                                     ItemName = s.ItemName,
                                     SingleItemWeight = s.SingleItemWeight,
                                     IsConnected = s.IsConnected,
                                     LatestReading = s.Readings
                                                      .OrderByDescending(r => r.Date)
                                                      .Select(r => new ReadingEntity
                                                      {
                                                          ReadId = r.ReadId,
                                                          Date = r.Date,
                                                          Value = r.Value,
                                                      })
                                                      .FirstOrDefault(),
                                     // Przeliczenie ilości sztuk, obsługa null
                                     Quantity = s.SingleItemWeight > 0 && s.Readings.Any()
                                                ? (decimal?)(s.Readings
                                                              .OrderByDescending(r => r.Date)
                                                              .Select(r => r.Value)
                                                              .FirstOrDefault() / s.SingleItemWeight)
                                                : (decimal?)null
                                 })
                                 .ToList();

            return result;
        }

        public IEnumerable<ScaleReadingDto> GetReadingsByScaleId(int scaleId)
        {
            var result = _context.Reading
                                 .Where(r => r.ScaleId == scaleId)
                                 .Select(r => new ScaleReadingDto
                                 {
                                     ScaleId = r.Scale.ScaleId,
                                     ScaleName = r.Scale.ScaleName,
                                     ItemName = r.Scale.ItemName,
                                     SingleItemWeight = r.Scale.SingleItemWeight,
                                     IsConnected = r.Scale.IsConnected,
                                     LatestReading = r, // Możesz zmienić nazwę na CurrentReading lub Reading
                                     Quantity = r.Value / r.Scale.SingleItemWeight
                                 })
                                 .ToList();

            return result;
        }

        public async Task<ReadingEntity> AddAsync(ReadingEntity readingEntity)
        {
            _context.Reading.Add(readingEntity);
            await _context.SaveChangesAsync();
            return readingEntity;
        }
    }
}
