using Athena.Models.Entities;
using Athena.ViewModels;
using AutoMapper;
using Xunit;

namespace Athena.Test
{
    public class MappingProfileTests
    {
        [Fact]
        public void TestTechniqueMappingProfile()
        {
            // Arrange
            var configuration = new MapperConfiguration(opt =>
                opt.CreateMap<Technique, TechniqueViewModel>());
            
            // Act

            // Assert
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestTechniqueTypeMappingProfile()
        {
            // Arrange
            var configuration = new MapperConfiguration(opt =>
                opt.CreateMap<TechniqueType, TechniqueTypeViewModel>());

            // Act

            // Assert
            configuration.AssertConfigurationIsValid();
        }


    }
}