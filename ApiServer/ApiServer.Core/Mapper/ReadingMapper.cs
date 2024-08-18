using ApiServer.Core.Dtos;
using ApiServer.Core.Entities;

namespace ApiServer.Core.Mapper
{
    public static class ReadingMapper
    {
        public static ReadingEntity MapToEntity(ReadingCreateDto dto)
        {
            return new ReadingEntity
            {
                Date = DateTime.Now,
                ScaleId = dto.ScaleId,
            };
        }
    }
}
