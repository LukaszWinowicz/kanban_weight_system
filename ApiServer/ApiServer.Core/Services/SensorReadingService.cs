﻿using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Mapper;

namespace ApiServer.Core.Services
{
    public class SensorReadingService : ISensorReadingService
    {
        private readonly ISensorReadingRepository _repository;

        public SensorReadingService(ISensorReadingRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SensorReadingEntity> GetAll()
        {
            var readings = _repository.GetAll();
            return readings;
        }
        public SensorReadingEntity GetById(int id)
        {
            var read = _repository.GetById(id);
            return read;
        }
        public int Create(SensorReadingCreateDto dto)
        {
            var create = _repository.Create(dto);
            return create;
        }
    }
}