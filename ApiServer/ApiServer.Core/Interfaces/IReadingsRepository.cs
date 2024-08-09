using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.Interfaces
{
    public interface IReadingsRepository
    {
        IEnumerable<SensorReadingEntity> GetAll();
        SensorReadingEntity GetById(int id);
        SensorReadingEntity GetLatestParamByName(string espName);
        IEnumerable<SensorReadingEntity> GetLatestSensorValue();
       // int Create(SensorReadingCreateDto dto);
    }
}
