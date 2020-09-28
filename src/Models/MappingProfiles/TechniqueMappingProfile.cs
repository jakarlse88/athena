using Athena.Models.Entities;
using Athena.Models.ViewModels;
using AutoMapper;

namespace Athena.Models.MappingProfiles
{
    public class TechniqueMappingProfile : Profile
    {
        public TechniqueMappingProfile()
        {
            CreateMap<Technique, TechniqueViewModel>();
        }
    }
}