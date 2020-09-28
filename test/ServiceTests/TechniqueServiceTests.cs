using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Models.MappingProfiles;
using Athena.Models.ViewModels;
using Athena.Repositories;
using Athena.Services;
using AutoMapper;
using Moq;
using Xunit;

namespace Athena.Test.ServiceTests
{
    public class TechniqueServiceTests
    {
        private readonly IMapper _mapper;

        public TechniqueServiceTests()
        {
            var config = new MapperConfiguration(opt => { opt.AddProfile(new TechniqueMappingProfile()); });

            _mapper = new Mapper(config);
        }

        /**
         * GetByName()
         */
        [Fact]
        public async Task TestGetByNameAsyncNameIllegalCharacters()
        {
            // Arrange
            const string testString = "t3chn1que";
            var service = new TechniqueService(null, null, null, null);

            // Act
            async Task<TechniqueViewModel> TestAction() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(TestAction);
            Assert.Equal("Argument contains one or more invalid characters. (Parameter 'name')", ex.Message);
            Assert.Equal("name", ex.ParamName);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task TestGetByNameAsyncNameNull(string testString)
        {
            // Arrange
            var service = new TechniqueService(null, null, null, null);

            // Act
            async Task<TechniqueViewModel> TestAction() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public async Task TestGetByNameAsyncEntityFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new[] { new Technique() })
                .Verifiable();

            var service = new TechniqueService(_mapper, mockRepository.Object, null, null);

            // Act
            var result = await service.GetByNameAsync("test");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueViewModel>(result);

            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task TestGetByNameAsyncEntityNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new List<Technique>())
                .Verifiable();

            var service = new TechniqueService(null, mockRepository.Object, null, null);

            // Act
            var result = await service.GetByNameAsync("test");

            // Assert
            Assert.Null(result);

            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()), Times.Once());
        }

        /**
         * CreateAsync()
         */
        [Fact]
        public async Task TestCreateAsyncModelNull()
        {
            // Arrange
            var service = new TechniqueService(null, null, null, null);

            // Act
            async Task<TechniqueViewModel> TestAction() =>
                await service.CreateAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("model", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsyncModelNameNull()
        {
            // Arrange
            var service = new TechniqueService(null, null, null, null);

            // Act
            async Task<TechniqueViewModel> TestAction() =>
                await service.CreateAsync(new TechniqueViewModel { Name = null });

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("Name", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            // Arrange
            var model = new TechniqueViewModel
            {
                TechniqueCategoryName = "Test",
                TechniqueTypeName = "Test",
                Name = "Arae-makgi"
            };

            var mockTechniqueRepository = new Mock<IRepository<Technique>>();
            mockTechniqueRepository
                .Setup(x => x.Insert(It.IsAny<Technique>()))
                .Verifiable();

            var mockTechniqueCategoryRepository = new Mock<IRepository<TechniqueCategory>>();
            mockTechniqueCategoryRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new List<TechniqueCategory> { new TechniqueCategory { Name = "Test" } })
                .Verifiable();

            var mockTechniqueTypeRepository = new Mock<IRepository<TechniqueType>>();
            mockTechniqueTypeRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new List<TechniqueType> { new TechniqueType { Name = "Test" } })
                .Verifiable();

            var service = new TechniqueService(_mapper, mockTechniqueRepository.Object,
                mockTechniqueTypeRepository.Object, mockTechniqueCategoryRepository.Object);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueViewModel>(result);
            Assert.Equal(model.Name, result.Name);

            mockTechniqueRepository
                .Verify(x => x.Insert(It.IsAny<Technique>()), Times.Once);

            mockTechniqueCategoryRepository
                .Verify(x =>
                        x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()),
                    Times.Once);

            mockTechniqueTypeRepository
                .Verify(x =>
                        x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()),
                    Times.Once);
        }

        [Fact]
        public async Task TestCreateAsyncTechniqueTypeNotFound()
        {
            // Arrange
            var model = new TechniqueViewModel
            {
                TechniqueCategoryName = "Test",
                TechniqueTypeName = "Test",
                Name = "Arae-makgi"
            };

            var mockTechniqueRepository = new Mock<IRepository<Technique>>();

            var mockTechniqueCategoryRepository = new Mock<IRepository<TechniqueCategory>>();
            mockTechniqueCategoryRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new List<TechniqueCategory> { new TechniqueCategory { Name = "Test" } })
                .Verifiable();

            var mockTechniqueTypeRepository = new Mock<IRepository<TechniqueType>>();
            mockTechniqueTypeRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new List<TechniqueType>())
                .Verifiable();

            var service = new TechniqueService(_mapper, mockTechniqueRepository.Object,
                mockTechniqueTypeRepository.Object, mockTechniqueCategoryRepository.Object);

            // Act
            async Task<TechniqueViewModel> TestAction() => await service.CreateAsync(model);

            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(TestAction);
            Assert.Equal(
                $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueType)} entity with the Name <{model.TechniqueTypeName}>.",
                ex.Message);

            mockTechniqueCategoryRepository
                .Verify(x =>
                        x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()),
                    Times.Once);

            mockTechniqueTypeRepository
                .Verify(x =>
                        x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()),
                    Times.Once);
        }

        [Fact]
        public async Task TestCreateAsyncTechniqueCategoryNotFound()
        {
            // Arrange
            var model = new TechniqueViewModel
            {
                TechniqueCategoryName = "Test",
                TechniqueTypeName = "Test",
                Name = "Arae-makgi"
            };

            var mockTechniqueRepository = new Mock<IRepository<Technique>>();

            var mockTechniqueCategoryRepository = new Mock<IRepository<TechniqueCategory>>();
            mockTechniqueCategoryRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new List<TechniqueCategory>())
                .Verifiable();

            var mockTechniqueTypeRepository = new Mock<IRepository<TechniqueType>>();

            var service = new TechniqueService(_mapper, mockTechniqueRepository.Object,
                mockTechniqueTypeRepository.Object, mockTechniqueCategoryRepository.Object);

            // Act
            async Task<TechniqueViewModel> TestAction() => await service.CreateAsync(model);

            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(TestAction);
            Assert.Equal(
                $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueCategory)} entity with the Name <{model.TechniqueCategoryName}>.",
                ex.Message);

            mockTechniqueCategoryRepository
                .Verify(x =>
                        x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()),
                    Times.Once);
        }

        /**
         * GetAll()
         */
        [Fact]
        public async Task TestGetAllNoResults()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new List<Technique>());

            var service = new TechniqueService(_mapper, mockRepository.Object, null, null);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<ICollection<TechniqueViewModel>>(result);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new List<Technique> { new Technique(), new Technique(), new Technique() });

            var service = new TechniqueService(_mapper, mockRepository.Object, null, null);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.IsAssignableFrom<ICollection<TechniqueViewModel>>(result);
        }
    }
}