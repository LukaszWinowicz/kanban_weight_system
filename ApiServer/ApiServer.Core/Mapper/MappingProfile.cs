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
                   src.Reading != null && src.SingleItemWeight > 0
                       ? (decimal?)Math.Floor(src.Reading.Value / src.SingleItemWeight)
                       : null
               )
           )
           .ForMember(
               dest => dest.ReadingDate,
               opt => opt.MapFrom(src => src.Reading != null ? src.Reading.Date : (DateTime?)null)
           )
           .ForMember(
               dest => dest.Value,
               opt => opt.MapFrom(src => src.Value)
           );
        }
    }
}
