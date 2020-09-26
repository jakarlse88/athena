using Athena.Models.Entities;
using Athena.Models.ViewModels;
using AutoMapper;

namespace Athena.Models.MappingProfiles
{
    public class TechniqueTypeMappingProfile : Profile
    {
        public TechniqueTypeMappingProfile()
        {
            CreateMap<TechniqueType, TechniqueTypeViewModel>();
        }
    }
}