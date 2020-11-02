using Athena.Models.DTOs;
using Athena.Models.Entities;
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
                opt.CreateMap<Technique, TechniqueDTO>());
            
            // Act

            // Assert
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestTechniqueTypeMappingProfile()
        {
            // Arrange
            var configuration = new MapperConfiguration(opt =>
                opt.CreateMap<TechniqueType, TechniqueTypeDTO>());

            // Act

            // Assert
            configuration.AssertConfigurationIsValid();
        }


    }
}