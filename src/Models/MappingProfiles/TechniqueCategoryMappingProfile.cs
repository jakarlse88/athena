using Athena.Models.DTOs;
using Athena.Models.Entities;
using AutoMapper;

namespace Athena.Models.MappingProfiles
{
    public class TechniqueCategoryMappingProfile : Profile
    {
        public TechniqueCategoryMappingProfile()
        {
            CreateMap<TechniqueCategory, TechniqueCategoryDTO>();
        }
    }
}