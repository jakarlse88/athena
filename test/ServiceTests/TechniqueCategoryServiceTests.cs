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
    public class TechniqueCategoryServiceTests
    {
        private readonly IMapper _mapper;

        public TechniqueCategoryServiceTests()
        {
            var configuration = new MapperConfiguration(profiles => profiles.AddProfile(new TechniqueCategoryMappingProfile()));
            
            _mapper = new Mapper(configuration);
        }
        
        /**
         * GetByNameAsync()
         */
        [Fact]
        public async Task TestGetByNameAsyncNameIllegalCharacters()
        {
            // Arrange
            const string testString = "t3chn1que";
            var service = new TechniqueCategoryService(null, null);

            // Act
            async Task<TechniqueCategoryViewModel> TestAction() => await service.GetByNameAsync(testString);

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
            var service = new TechniqueCategoryService(null, null);

            // Act
            async Task<TechniqueCategoryViewModel> TestAction() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public async Task TestGetByNameAsyncEntityFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueCategory>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new[] { new TechniqueCategory() })
                .Verifiable();

            var service = new TechniqueCategoryService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetByNameAsync("test");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueCategoryViewModel>(result);

            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task TestGetByNameAsyncEntityNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueCategory>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new List<TechniqueCategory>())
                .Verifiable();

            var service = new TechniqueCategoryService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetByNameAsync("test");

            // Assert
            Assert.Null(result);

            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()), Times.Once());
        }
        
        /**
         * Create
         */
        [Fact]
        public async Task TestCreateAsyncModelNull()
        {
            // Arrange
            var service = new TechniqueCategoryService(null, null);

            // Act
            async Task<TechniqueCategoryViewModel> TestAction() =>
                await service.CreateAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("model", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsyncModelNameNull()
        {
            // Arrange
            var service = new TechniqueCategoryService(null, null);

            // Act
            async Task<TechniqueCategoryViewModel> TestAction() =>
                await service.CreateAsync(new TechniqueCategoryViewModel { Name = null });

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("Name", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            // Arrange
            var model = new TechniqueCategoryViewModel
            {
                Name = "Block"
            };

            var mockTechniqueCategoryRepository = new Mock<IRepository<TechniqueCategory>>();
            mockTechniqueCategoryRepository
                .Setup(x => x.Insert(It.IsAny<TechniqueCategory>()))
                .Verifiable();

            var service = new TechniqueCategoryService(mockTechniqueCategoryRepository.Object, _mapper);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueCategoryViewModel>(result);
            Assert.Equal(model.Name, result.Name);

            mockTechniqueCategoryRepository
                .Verify(x => x.Insert(It.IsAny<TechniqueCategory>()), Times.Once());
        }
        
        /**
         * GetAll()
         */
        [Fact]
        public async Task TestGetAllNoResults()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueCategory>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new List<TechniqueCategory>());

            var service = new TechniqueCategoryService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<ICollection<TechniqueCategoryViewModel>>(result);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueCategory>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueCategory, bool>>>()))
                .ReturnsAsync(new List<TechniqueCategory> { new TechniqueCategory(), new TechniqueCategory(), new TechniqueCategory() });

            var service = new TechniqueCategoryService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.IsAssignableFrom<ICollection<TechniqueCategoryViewModel>>(result);
        }
    }
}