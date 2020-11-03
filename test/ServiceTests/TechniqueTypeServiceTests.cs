using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Infrastructure.Exceptions;
using Athena.Models.DTOs;
using Athena.Models.Entities;
using Athena.Models.MappingProfiles;
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
            async Task<TechniqueTypeDTO> Action() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
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
            Assert.IsAssignableFrom<TechniqueTypeDTO>(result);

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
            async Task<TechniqueTypeDTO> Action() => await service.GetByNameAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(Action);
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
            async Task<TechniqueTypeDTO> Action() =>
                await service.CreateAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("model", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsyncModelNameNull()
        {
            // Arrange
            var service = new TechniqueTypeService(null, null);

            // Act
            async Task<TechniqueTypeDTO> Action() =>
                await service.CreateAsync(new TechniqueTypeDTO(null, null, null, null));

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("Name", ex.ParamName);
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            // Arrange
            var model = new TechniqueTypeDTO("Arae-makgi", null, null, null);

            var mockTechniqueTypeRepository = new Mock<IRepository<TechniqueType>>();
            mockTechniqueTypeRepository
                .Setup(x => x.Insert(It.IsAny<TechniqueType>()))
                .ReturnsAsync(new TechniqueType { Name = model.Name })
                .Verifiable();

            var service = new TechniqueTypeService(mockTechniqueTypeRepository.Object, _mapper);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueTypeDTO>(result);
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
            Assert.IsAssignableFrom<ICollection<TechniqueTypeDTO>>(result);
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
            Assert.IsAssignableFrom<ICollection<TechniqueTypeDTO>>(result);
        }
        
        /**
         * DeleteAsync()
         */
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task TestDeleteAsyncEntityNameNull(string testString)
        {
            // Arrange
            var service = new TechniqueTypeService(null, null);

            // Act
            async Task Action() => await service.DeleteAsync(testString);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("entityName", ex.ParamName);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueType>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new TechniqueType[] { new() })
                .Verifiable();

            mockRepository
                .Setup(x => x.DeleteAsync(It.IsAny<TechniqueType>()))
                .Verifiable();

            var service = new TechniqueTypeService(mockRepository.Object, _mapper);

            // Act
            await service.DeleteAsync("test");

            // Assert
            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()),
                    Times.Once());

            mockRepository
                .Verify(x => x.DeleteAsync(It.IsAny<TechniqueType>()), Times.Once());
        }

        [Fact]
        public async Task TestDeleteAsyncEntityNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TechniqueType>>();
            mockRepository
                .Setup(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()))
                .ReturnsAsync(new List<TechniqueType>())
                .Verifiable();

            var service = new TechniqueTypeService(mockRepository.Object, _mapper);

            // Act
            async Task Action() => await service.DeleteAsync("test");

            // Assert
            var ex = await Assert.ThrowsAsync<EntityNotFoundException>(Action);
            Assert.Equal("Couldn't find an entity matching the specified name", ex.Message);
            Assert.Equal("TechniqueType", ex.EntityType);
            Assert.Equal("test", ex.EntityName);
            
            mockRepository
                .Verify(x => x.GetByConditionAsync(It.IsAny<Expression<Func<TechniqueType, bool>>>()),
                    Times.Once());
        }
    }
}