using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Mapper
{
    public static class SensorReadingMapper
    {
        // Mapowanie DTO -> Entity
        public static SensorReadingEntity ToEntity(SensorReadingCreateDto createDto)
        {
            return new SensorReadingEntity
            {
                Date = DateTime.UtcNow, // Ustawiamy aktualną datę
                EspName = createDto.EspName,
                EspId = createDto.EspId,
                Value = createDto.Value

            };
        }
    }
}
