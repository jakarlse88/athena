using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.Data;
using Athena.Models.NewEntities;
using Athena.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Xunit;

namespace Athena.Test.RepositoryTests
{
    public class TechniqueRepositoryTests
    {
        private readonly IEnumerable<Technique> _techniques;

        public TechniqueRepositoryTests()
        {
            _techniques = new[]
            {
                new Technique
                {
                    Id = 1,
                    Name = "Arae-makgi"
                },
                new Technique
                {
                    Id = 2,
                    Name = "Momtong an-makgi"
                },
                new Technique
                {
                    Id = 3, Name = "Eolgul-makgi"
                }
            };
        }

        /**
         * GetByCondition
         */
        
        [Fact]
        public async Task TestGetByCondition()
        {
            // Arrange
            var options = GenerateInMemoryDbContextOptions();

            IEnumerable<Technique> results;

            await using (var context = new AthenaDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                SeedInMemoryTestDb(context);

                var repository = new Repository<Technique>(context);

                // Act
                results = await repository.GetByConditionAsync(_ => true);

                await context.Database.EnsureDeletedAsync();
            }

            // Assert
            Assert.NotNull(results);
            Assert.IsAssignableFrom<IEnumerable<Technique>>(results);
            Assert.Equal(_techniques.Count(), results.Count());
            Assert.Equal("Arae-makgi", results.First().Name);
        }

        [Fact]
        public async Task TestGetByConditionPredicateNull()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(null);
            
            // Act
            async Task<IEnumerable<Technique>> TestAction() => 
                await unitOfWork.TechniqueRepository.GetByConditionAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
            Assert.Equal("predicate", ex.ParamName);
        }

        /**
         * GetById
         */
        
        [Fact]
        public async Task TestGetBydIdOutOfRange()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(null);
            
            // Act
            async Task TestAction() => await unitOfWork.TechniqueRepository.GetByIdAsync(0);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(TestAction);
            Assert.Equal("id", ex.ParamName);
        }

        [Fact]
        public async Task TestGetByIdValid()
        {
            // Arrange
            var options = GenerateInMemoryDbContextOptions();

            Technique result;

            await using (var context = new AthenaDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                
                SeedInMemoryTestDb(context);

                var repository = new Repository<Technique>(context);

                // Act
                result = await repository.GetByIdAsync(1);

                await context.Database.EnsureDeletedAsync();
            }

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Technique>(result);
            Assert.Equal("Arae-makgi", result.Name);
        }

        [Fact]
        public async Task TestGetByIdInvalid()
        {
            // Arrange
            var options = GenerateInMemoryDbContextOptions();

            Technique result = null;

            await using (var context = new AthenaDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                
                SeedInMemoryTestDb(context);

                var repository = new Repository<Technique>(context);

                // Act
                result = await repository.GetByIdAsync(1);

                await context.Database.EnsureDeletedAsync();
            }

            // Assert
            Assert.NotNull(result);
        }
        
        /**
         * Add()
         */
        [Fact]
        public void TestAddEntityNull()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(null);

            // Act
            void TestAction() => unitOfWork.TechniqueRepository.Add(null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(TestAction);
            Assert.Equal("entity", ex.ParamName);
        }

        [Fact]
        public void TestAddEntityValid()
        {
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Set<Technique>().Add(It.IsAny<Technique>()))
                .Verifiable();

            var repository = new Repository<Technique>(mockContext.Object);
            
            var entity = new Technique();
            
            // Act
            repository.Add(entity);

            // Assert
            mockContext
                .Verify(x => x.Set<Technique>().Add(It.IsAny<Technique>()), Times.Once);
        }



        /**
         * Internal helper methods
         */
        void SeedInMemoryTestDb(AthenaDbContext context)
        {
            context.AddRange(_techniques);

            context.SaveChanges();
        }
        
        private static DbContextOptions<AthenaDbContext> GenerateInMemoryDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<AthenaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
                .Options;
            return options;
        }
    }
}