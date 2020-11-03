using Athena.Models.DTOs;
using Athena.Models.Entities;
using AutoMapper;

namespace Athena.Models.MappingProfiles
{
    public class TechniqueTypeMappingProfile : Profile
    {
        public TechniqueTypeMappingProfile()
        {
            CreateMap<TechniqueType, TechniqueTypeDTO>();
        }
    }
}