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
        }
    }
}
