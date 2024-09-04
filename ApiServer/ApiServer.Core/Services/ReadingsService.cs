using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using AutoMapper;

namespace ApiServer.Core.Services
{
    public class ReadingsService : IReadingsService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IReadingsRepository _readingsRepository;
        private readonly IMapper _mapper;

        public ReadingsService(IScaleRepository scaleRepository, IReadingsRepository readingsRepository, IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _readingsRepository = readingsRepository;
            _mapper = mapper;
        }

        public IEnumerable<ScaleReadingDto> GetLatestReadingForEveryScale()
        {
            var scalesValues = _scaleRepository.GetAll();
            var readingsValues = _readingsRepository.GetAll();

            var latestReadings = scalesValues
                .GroupJoin(
                    readingsValues,
                    scale => scale.ScaleName,
                    reading => reading.ScaleName,
                    (scale, readings) => new ScaleReadingEntity
                    {
                        ScaleName = scale.ScaleName,
                        ItemName = scale.ItemName,
                        SingleItemWeight = scale.SingleItemWeight,
                        IsConnected = scale.IsConnected,
                        LatestReading = readings
                            .OrderByDescending(r => r.Date)
                            .FirstOrDefault()
                    }
                )
                .ToList();

            var dtoList = _mapper.Map<IEnumerable<ScaleReadingDto>>(latestReadings);
            return dtoList;
        }
    }
}
