﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            async Task<IEnumerable<Technique>> TestAction() => 
                await repository.GetByConditionAsync(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
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
            async Task TestAction() => await repository.Insert(null);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(TestAction);
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
            async Task<Technique> TestAction() => await repository.Insert(entity);

            // Assert
            var ex = await Assert.ThrowsAsync<Exception>(TestAction);
            
            mockContext
                .Verify(x => x.Add(It.IsAny<Technique>()), Times.Once);
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