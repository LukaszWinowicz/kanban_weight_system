using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Infrastructure.Repositories
{
    public class SensorReadingRepository : ISensorReadingRepository
    {
        private readonly ApiServerContext _context;

        public SensorReadingRepository(ApiServerContext context) 
        {
            _context = context;
        }

        public async Task<SensorReading> AddAsync(SensorReading sensorReading)
        {
            await _context.SensorReadings.AddAsync(sensorReading);
            await _context.SaveChangesAsync();
            return sensorReading;
        }

    }
}
