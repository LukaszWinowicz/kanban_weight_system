using ApiServer.Core.DTOs;
using ApiServer.Core.Entities;
using AutoMapper;

namespace ApiServer.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ScaleEntity, ScaleDto>();
            CreateMap<ScaleReadingEntity, ScaleReadingDto>()
           .ForMember(
               dest => dest.Quantity,
               opt => opt.MapFrom(src =>
                   src.LatestReading != null && src.SingleItemWeight > 0
                       ? (decimal?)(src.LatestReading.Value / src.SingleItemWeight)
                       : null
               )
           )
           .ForMember(
               dest => dest.LatestReadingDate,
               opt => opt.MapFrom(src => src.LatestReading != null ? src.LatestReading.Date : (DateTime?)null)
           );
        }
    }
}
