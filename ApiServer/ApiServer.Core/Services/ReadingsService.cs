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
                    (scale, readings) =>
                    {
                        var latestReading = readings
                            .OrderByDescending(r => r.Date)
                            .FirstOrDefault();

                        return new ScaleReadingEntity
                        {
                            ScaleName = scale.ScaleName,
                            ItemName = scale.ItemName,
                            SingleItemWeight = scale.SingleItemWeight,
                            Reading = latestReading,
                            Value = latestReading?.Value
                        };
                    }
                )
                .ToList();

            var dtoList = _mapper.Map<IEnumerable<ScaleReadingDto>>(latestReadings);
            return dtoList;
        }

        public IEnumerable<ScaleReadingDto> GetAllReadingsByScaleName(string scaleName)
        {
            var scalesValues = _scaleRepository.GetAll();
            var readingsValues = _readingsRepository.GetAll();

            var readings = readingsValues
                .Where(r => r.ScaleName == scaleName)
                .Select(r => new ScaleReadingEntity
                 {
                     ScaleName = r.Scale.ScaleName,
                     ItemName = r.Scale.ItemName,
                     SingleItemWeight = r.Scale.SingleItemWeight,
                     Reading = r,
                     Value = r?.Value,
                })
                .ToList();

            var dtoList = _mapper.Map<IEnumerable<ScaleReadingDto>>(readings);
            return dtoList;
        }
    }
}
