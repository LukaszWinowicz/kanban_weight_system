﻿using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleService
    {
        IEnumerable<ScaleEntity> GetAll();
        IEnumerable<ScaleWithAllReadingsDto> G();
    }
}