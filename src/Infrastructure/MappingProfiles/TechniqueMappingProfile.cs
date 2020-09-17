using Athena.Models.Entities;
using Athena.ViewModels;
using AutoMapper;

namespace Athena.Infrastructure.MappingProfiles
{
    public class TechniqueMappingProfile : Profile
    {
        public TechniqueMappingProfile()
        {
            CreateMap<Technique, TechniqueViewModel>();
        }
    }
}