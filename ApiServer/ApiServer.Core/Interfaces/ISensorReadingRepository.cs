using ApiServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.Interfaces
{
    public interface ISensorReadingRepository
    {
        Task<SensorReading> AddAsync(SensorReading sensorReading);
        // Dodamy więcej metod później dla innych operacji CRUD
    }
}
