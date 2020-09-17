using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Infrastructure.MappingProfiles;
using Athena.Models.Entities;
using Athena.Repositories;
using Athena.Services;
using Athena.ViewModels;
using AutoMapper;
using Moq;
using Xunit;

namespace Athena.Test.ServiceTests
{
    public class TechniqueTypeServiceTests
    {
        private readonly IMapper _mapper;

        public TechniqueTypeServiceTests()
        {
            var config = new MapperConfiguration(opt => { opt.AddProfile(new TechniqueTypeMappingProfile()); });

            _mapper = new Mapper(config);
        }

        /**
         * Create
         */
        [Fact]
        public async Task TestCreateAsyncModelNull()
        {
            // Arrange
            var service = new TechniqueTypeService(null, null);

            // Act
            async Task<TechniqueTypeViewModel> TestAction() =>
                await service.CreateAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("model", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsyncModelNameNull()
        {
            // Arrange
            var service = new TechniqueTypeService(null, null);

            // Act
            async Task<TechniqueTypeViewModel> TestAction() =>
                await service.CreateAsync(new TechniqueTypeViewModel { Name = null });

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("Name", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            // Arrange
            var model = new TechniqueTypeViewModel
            {
                Name = "Arae-makgi"
            };

            var mockTechniqueTypeRepository = new Mock<IRepository<TechniqueType>>();
            mockTechniqueTypeRepository
                .Setup(x => x.Insert(It.IsAny<TechniqueType>()))
                .Verifiable();

            var service = new TechniqueTypeService(mockTechniqueTypeRepository.Object, _mapper);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueTypeViewModel>(result);
            Assert.Equal(model.Name, result.Name);

            mockTechniqueTypeRepository
                .Verify(x => x.Insert(It.IsAny<TechniqueType>()), Times.Once());
        }
    }
}