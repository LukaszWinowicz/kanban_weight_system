﻿using ApiServer.Core.Dtos;
using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Core.Mapper;
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

        public IEnumerable<ReadingEntity> GetAll()
        {
            var readings = _context.ReadingEntities.ToList();
            return readings;
        }

        public ReadingEntity GetById(int id)
        {
            var read = _context.ReadingEntities.Find(id);
            return read;
        }

        public ReadingEntity GetLatestParamByName(int scaleId)
        {
            var value = _context.ReadingEntities
                                .Where(x => x.ScaleId == scaleId)
                                .OrderByDescending(t => t.Date)
                                .FirstOrDefault();
            return value;
        }

        public IEnumerable<ReadingEntity> GetLatestSensorValue()
        {
            var value = _context.ReadingEntities
                                .GroupBy(s => s.ScaleId)
                                .Select(g => g.OrderByDescending(d => d.Date).First())
                                .ToList();
            return value;
        }

        public int Create(ReadingCreateDto dto)
        {
            var entity = ReadingMapper.MapToEntity(dto);
            _context.ReadingEntities.Add(entity);
            _context.SaveChanges();
            return entity.ReadId;
        }
    }
}