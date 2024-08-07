using ApiServer.Api.Models;
using ApiServer.Core.Entities;

namespace ApiServer.API.Mapper
{
    public class SensorReadingMapper
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
