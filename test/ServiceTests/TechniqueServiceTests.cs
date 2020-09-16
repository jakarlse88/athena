using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Infrastructure.MappingProfiles;
using Athena.Models.NewEntities;
using Athena.Repositories;
using Athena.Services;
using Athena.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public async Task TestCreateAsyncModelNull()
        {
            // Arrange
            var service = new TechniqueService(null, null);

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
            var service = new TechniqueService(null, null);

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
                TechniqueCategoryId = 1,
                TechniqueTypeId = 1,
                Name = "Arae-makgi"
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TechniqueRepository.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new List<Technique>())
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.TechniqueRepository.Add(It.IsAny<Technique>()))
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.TechniqueCategoryRepository.GetByIdAsync(1))
                .ReturnsAsync(new TechniqueCategory { Id = 1 })
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.TechniqueTypeRepository.GetByIdAsync(1))
                .ReturnsAsync(new TechniqueType { Id = 1 })
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.CommitAsync())
                .Verifiable();

            var service = new TechniqueService(mockUnitOfWork.Object, _mapper);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TechniqueViewModel>(result);
            Assert.Equal(model.Name, result.Name);

            mockUnitOfWork
                .Verify(x =>
                        x.TechniqueRepository.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()),
                    Times.Once);

            mockUnitOfWork
                .Verify(x => x.TechniqueRepository.Add(It.IsAny<Technique>()),
                    Times.Once);

            mockUnitOfWork
                .Verify(x => x.TechniqueCategoryRepository.GetByIdAsync(1), Times.Once());

            mockUnitOfWork
                .Verify(x => x.TechniqueTypeRepository.GetByIdAsync(1), Times.Once());

            mockUnitOfWork
                .Verify(x => x.CommitAsync(), Times.Once());
        }

        [Fact]
        public async Task TestCreateAsyncException()
        {
            // Arrange
            var model = new TechniqueViewModel
            {
                TechniqueCategoryId = 1,
                TechniqueTypeId = 1,
                Name = "Arae-makgi"
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TechniqueRepository.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new List<Technique>())
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.TechniqueRepository.Add(It.IsAny<Technique>()))
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.TechniqueCategoryRepository.GetByIdAsync(1))
                .ThrowsAsync(new ArgumentNullException())
                .Verifiable();

            mockUnitOfWork
                .Setup(x => x.RollbackAsync())
                .Verifiable();

            var service = new TechniqueService(mockUnitOfWork.Object, _mapper);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.Null(result);

            mockUnitOfWork
                .Verify(x =>
                        x.TechniqueRepository.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()),
                    Times.Once);

            mockUnitOfWork
                .Verify(x => x.TechniqueCategoryRepository.GetByIdAsync(1), Times.Once());

            mockUnitOfWork
                .Verify(x => x.RollbackAsync(), Times.Once());
        }

        [Fact]
        public async Task TestCreateDuplicateName()
        {
            // Arrange
            var model = new TechniqueViewModel
            {
                TechniqueCategoryId = 1,
                TechniqueTypeId = 1,
                Name = "Arae-makgi"
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.TechniqueRepository.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()))
                .ReturnsAsync(new List<Technique> { new Technique() })
                .Verifiable();

            var service = new TechniqueService(mockUnitOfWork.Object, _mapper);

            // Act
            var result = await service.CreateAsync(model);

            // Assert
            Assert.Null(result);

            mockUnitOfWork
                .Verify(x =>
                        x.TechniqueRepository.GetByConditionAsync(It.IsAny<Expression<Func<Technique, bool>>>()),
                    Times.Once);
        }
    }
}