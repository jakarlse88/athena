using System.Threading;
using System.Threading.Tasks;
using Athena.Data;
using Athena.Models.NewEntities;
using Athena.Repositories;
using Moq;
using Xunit;

namespace Athena.Test.RepositoryTests
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void TestTechniqueRepository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(Mock.Of<AthenaDbContext>());
            
            // Act
            var repository = unitOfWork.TechniqueRepository;
            
            // Assert
            Assert.NotNull(repository);
            Assert.IsAssignableFrom<Repository<Technique>>(repository);
        }
        
        [Fact]
        public void TestTechniqueCategoryRepository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(Mock.Of<AthenaDbContext>());
            
            // Act
            var repository = unitOfWork.TechniqueCategoryRepository;
            
            // Assert
            Assert.NotNull(repository);
            Assert.IsAssignableFrom<Repository<TechniqueCategory>>(repository);
        }
        
        [Fact]
        public void TestTechniqueTypeRepository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(Mock.Of<AthenaDbContext>());
            
            // Act
            var repository = unitOfWork.TechniqueTypeRepository;
            
            // Assert
            Assert.NotNull(repository);
            Assert.IsAssignableFrom<Repository<TechniqueType>>(repository);
        }
        
        [Fact]
        public async Task TestCommitAsync()
        {
            // Arrange
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Verifiable();

            var unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            await unitOfWork.CommitAsync();

            // Assert
            mockContext
                .Verify(x =>
                        x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public async Task TestRollbackAsync()
        {
            // Arrange
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.DisposeAsync())
                .Verifiable();

            var unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            await unitOfWork.RollbackAsync();

            // Assert
            mockContext
                .Verify(x =>
                        x.DisposeAsync(),
                    Times.Once);
        }

    }
}