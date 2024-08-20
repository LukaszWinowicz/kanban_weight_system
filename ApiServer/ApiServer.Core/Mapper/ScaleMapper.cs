using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Mapper
{
    public class ScaleMapper
    {
        public static ScaleEntity MapToEntity(ScaleCreateDto dto)
        {
            return new ScaleEntity
            {
                ScaleName = dto.ScaleName,
                ItemName = dto.ItemName,
                SingleItemWeight = dto.SingleItemWeight,
                IsConnected = dto.IsConnected,
            };
        }
    }
}
