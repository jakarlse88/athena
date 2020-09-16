using Athena.Models.NewEntities;
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

    }
}