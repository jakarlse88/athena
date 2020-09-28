using Athena.Models.Entities;
using Athena.Models.ViewModels;
using AutoMapper;

namespace Athena.Models.MappingProfiles
{
    public class TechniqueCategoryMappingProfile : Profile
    {
        public TechniqueCategoryMappingProfile()
        {
            CreateMap<TechniqueCategory, TechniqueCategoryViewModel>();
        }
    }
}