using Athena.Models.Entities;
using Athena.ViewModels;
using AutoMapper;

namespace Athena.Infrastructure.MappingProfiles
{
    public class TechniqueTypeMappingProfile : Profile
    {
        public TechniqueTypeMappingProfile()
        {
            CreateMap<TechniqueType, TechniqueTypeViewModel>();
        }
    }
}