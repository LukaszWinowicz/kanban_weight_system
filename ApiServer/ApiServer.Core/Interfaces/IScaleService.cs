﻿using ApiServer.Core.DTOs;

namespace ApiServer.Core.Interfaces
{
    public interface IScaleService
    {
        IEnumerable<ScaleDto> GetAll();
        IEnumerable<ScaleDto> GetScalesWithAnyReadings();
        bool CreateScale(ScaleCreateDto scale);
        bool DeleteScale(string scaleName);
    }
}
