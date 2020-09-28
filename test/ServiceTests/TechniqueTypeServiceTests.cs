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
    public class TechniqueTypeServiceTests
    {
        private readonly IMapper _mapper;

        public TechniqueTypeServiceTests()
        {
            var config = new MapperConfiguration(opt => { opt.AddProfile(new TechniqueTypeMappingProfile()); });

            _mapper = new Mapper(config);
        }

        /**
         * GetByName()
         */
        
        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task TestGetByNameAsyncNameNull(string testString)
        {
            // Arrange
            var service = new TechniqueTypeService(null, null);

            // Act
            async Task<TechniqueTypeViewModel> TestAction() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public async Task TestGetByNameAsyncEntityFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueType>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new[] { new TechniqueType() })
                .Verifiable();

            var service = new TechniqueTypeService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetByNameAsync("test");

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueTypeViewModel>(result);

            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task TestGetByNameAsyncEntityNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueType>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new List<TechniqueType>())
                .Verifiable();

            var service = new TechniqueTypeService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetByNameAsync("test");

            // Assert
            Assert.Null(result);

            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()), Times.Once());
        }

        [Fact]
        public async Task TestGetByNameAsyncNameIllegalCharacters()
        {
            // Arrange
            const string testString = "t3chn1que";
            var service = new TechniqueTypeService(null, null);

            // Act
            async Task<TechniqueTypeViewModel> TestAction() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(TestAction);
            Assert.Equal("Argument contains one or more invalid characters. (Parameter 'name')", ex.Message);
            Assert.Equal("name", ex.ParamName);
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
        
        /**
         * GetAllAsync()
         */
        [Fact]
        public async Task TestGetAllNoResults()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueType>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new List<TechniqueType>());

            var service = new TechniqueTypeService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<ICollection<TechniqueTypeViewModel>>(result);
        }

        [Fact]
        public async Task TestGetAll()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueType>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new List<TechniqueType> { new TechniqueType(), new TechniqueType(), new TechniqueType() });

            var service = new TechniqueTypeService(mockRepository.Object, _mapper);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.IsAssignableFrom<ICollection<TechniqueTypeViewModel>>(result);
        }
    }
}