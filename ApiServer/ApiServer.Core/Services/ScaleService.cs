using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using AutoMapper;

namespace ApiServer.Core.Services
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleRepository _repository;
        private readonly IMapper _mapper;

        public ScaleService(IScaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<ScaleDto> GetAll()
        {
            var entity = _repository.GetAll();
            var dto = _mapper.Map<IEnumerable<ScaleDto>>(entity);
            return dto;
        }

        public IEnumerable<ScaleDto> GetScalesWithAnyReadings()
        {
            var entity = _repository.GetScalesWithAnyReadings();
            var dto = _mapper.Map<IEnumerable<ScaleDto>>(entity);
            return dto;
        }
    }
}
