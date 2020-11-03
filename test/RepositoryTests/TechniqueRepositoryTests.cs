using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.Data;
using Athena.Models.Entities;
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
                    Name = "Arae-makgi"
                },
                new Technique
                {
                    Name = "Momtong an-makgi"
                },
                new Technique
                {
                    Name = "Eolgul-makgi"
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
            var repository = new Repository<Technique>(null);

            // Act
            async Task<IEnumerable<Technique>> Action() =>
                await repository.GetByConditionAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("predicate", ex.ParamName);
        }

        /**
         * Insert()
         */
        [Fact]
        public async Task TestInsertAsyncNull()
        {
            // Arrange
            var repository = new Repository<Technique>(null);

            // Act
            async Task Action() => await repository.Insert(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("entity", ex.ParamName);
        }

        [Fact]
        public void TestInsertAsyncValid()
        {
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Add(It.IsAny<Technique>()))
                .Verifiable();

            var repository = new Repository<Technique>(mockContext.Object);

            var entity = new Technique();

            // Act
            repository.Insert(entity);

            // Assert
            mockContext
                .Verify(x => x.Add(It.IsAny<Technique>()), Times.Once);
        }

        [Fact]
        public async Task TestInsertAsyncThrows()
        {
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Add(It.IsAny<Technique>()))
                .Throws<Exception>()
                .Verifiable();

            var repository = new Repository<Technique>(mockContext.Object);

            var entity = new Technique();

            // Act
            async Task<Technique> Action() => await repository.Insert(entity);

            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(Action);

            mockContext
                .Verify(x => x.Add(It.IsAny<Technique>()), Times.Once);
        }

        /**
         * Update()
         */
        [Fact]
        public async Task TestUpdateEntityNull()
        {
            // Arrange
            var repository = new Repository<Technique>(null);

            // Act
            async Task Action() => await repository.UpdateAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("entity", ex.ParamName);
        }

        [Fact]
        public async Task TestUpdate()
        {
            // Arrange
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Set<Technique>().Update(It.IsAny<Technique>()))
                .Verifiable();

            var repository = new Repository<Technique>(mockContext.Object);
            
            // Act
            await repository.UpdateAsync(new Technique());

            // Assert
            mockContext
                .Verify(x => x.Set<Technique>().Update(It.IsAny<Technique>()), Times.Once());
        }

        [Fact]
        public async Task TestUpdateAsyncThrows()
        {
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Set<Technique>().Update(It.IsAny<Technique>()))
                .Throws<Exception>();

            var repository = new Repository<Technique>(mockContext.Object);

            var entity = new Technique();

            // Act
            async Task Action() => await repository.UpdateAsync(entity);

            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(Action);

            mockContext
                .Verify(x => x.Set<Technique>().Update(It.IsAny<Technique>()), Times.Once);
        }
        
        /**
         * DeleteAsync()
         */
        [Fact]
        public async Task TestDeleteAsyncEntityNull()
        {
            // Arrange
            var repository = new Repository<Technique>(null);
            
            // Act
            async Task Action() => await repository.DeleteAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(Action);
            Assert.Equal("entity", ex.ParamName);
        }

        [Fact]
        public async Task TestDeleteAsyncRepositoryThrows()
        {
            // Arrange
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Set<Technique>().Remove(It.IsAny<Technique>()))
                .Throws<Exception>();

            var repository = new Repository<Technique>(mockContext.Object);
            
            // Act
            async Task Action() => await repository.DeleteAsync(new Technique());

            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(Action);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            // Arrange
            var mockContext = new Mock<AthenaDbContext>();
            mockContext
                .Setup(x => x.Set<Technique>().Remove(It.IsAny<Technique>()))
                .Verifiable();

            var repository = new Repository<Technique>(mockContext.Object);
            
            // Act
            await repository.DeleteAsync(new Technique());

            // Assert
            mockContext
                .Verify(x => x.Set<Technique>().Remove(It.IsAny<Technique>()), Times.Once());
        }




        /**
         * Internal helper methods
         */
        private void SeedInMemoryTestDb(AthenaDbContext context)
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