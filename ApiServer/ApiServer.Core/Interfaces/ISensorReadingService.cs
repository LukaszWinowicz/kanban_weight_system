﻿using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.Interfaces
{
    public interface ISensorReadingService
    {
        public IEnumerable<SensorReadingEntity> GetAll();
        SensorReadingEntity GetById(int id);
        int Create(SensorReadingCreateDto dto);
    }
}