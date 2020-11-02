using Athena.Models.DTOs;
using Athena.Models.Entities;
using AutoMapper;

namespace Athena.Models.MappingProfiles
{
    public class TechniqueMappingProfile : Profile
    {
        public TechniqueMappingProfile()
        {
            CreateMap<Technique, TechniqueDTO>();
        }
    }
}