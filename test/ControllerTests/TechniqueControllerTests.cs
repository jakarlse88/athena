using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.Controllers;
using Athena.Infrastructure.Exceptions;
using Athena.Models.DTOs;
using Athena.Models.Entities;
using Athena.Services;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Athena.Test.ControllerTests
{
    public class TechniqueControllerTests
    {
        /**
         * All actions should have authorize attributes
         */
        [Fact]
        public void TestHasAuthorizeAttribute()
        {
            // Arrange
            var controller = new TechniqueController(null);

            // Act
            var methods = controller.GetType().GetDeclaredMethods();

            foreach (var method in methods)
            {
                // Assert
                Assert.Equal(typeof(AuthorizeAttribute),
                    method.GetCustomAttributes(typeof(AuthorizeAttribute), true).First().GetType());
            }
        }

        /**
         * Get()
         */
        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("Test!")]
        [InlineData(" &%&&")]
        [InlineData("t3st")]
        public async Task TestGetInvalidName(string testName)
        {
            // Arrange
            var controller = new TechniqueController(null);

            // Act
            var result = await controller.Get(testName);

            // Assert
            var actionResult = Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestGetEntityFound()
        {
            // Arrange
            const string testName = "Test";

            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.GetByNameAsync(testName))
                .ReturnsAsync(new TechniqueDTO(testName, "", "", "", "", ""))
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);

            // Act
            var result = await controller.Get(testName);

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var modelResult = Assert.IsAssignableFrom<TechniqueDTO>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task TestGetEntityNotFound()
        {
            // Arrange
            const string testName = "Test";

            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.GetByNameAsync(testName))
                .ReturnsAsync(new TechniqueDTO(testName, "", "", "", "", ""))
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);

            // Act
            var result = await controller.Get("Incorrect name");

            // Assert
            var actionResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);

            mockService
                .Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once());
        }

        /**
         * Post()
         */
        [Fact]
        public async Task TestPostModelNull()
        {
            // Arrange
            var controller = new TechniqueController(null);

            // Act
            var result = await controller.Post(null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestPostModelNotNull()
        {
            // Arrange
            var testName = "test";

            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(ms => ms.CreateAsync(It.IsAny<TechniqueDTO>()))
                .ReturnsAsync(new TechniqueDTO(testName, "", "", "", "", ""))
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);

            // Act
            var result = await controller.Post(new TechniqueDTO("", "", "", "", "", ""));

            // Assert
            var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal("Get", actionResult.ActionName);
            var modelResult = Assert.IsAssignableFrom<TechniqueDTO>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(ms => ms.CreateAsync(It.IsAny<TechniqueDTO>()), Times.Once);
        }

        /**
         * Get() 
         */
        [Fact]
        public async Task TestGetNoResults()
        {
            // Arrange
            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<TechniqueDTO>());

            var controller = new TechniqueController(mockService.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var modelResult = Assert.IsAssignableFrom<ICollection<TechniqueDTO>>(actionResult.Value);
            Assert.IsAssignableFrom<ICollection<TechniqueDTO>>(modelResult);
            Assert.Empty(modelResult);
        }

        [Fact]
        public async Task TestGet()
        {
            // Arrange
            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<TechniqueDTO>
                {
                    new TechniqueDTO("", "", "", "", "", ""),
                    new TechniqueDTO("", "", "", "", "", ""),
                    new TechniqueDTO("", "", "", "", "", "")
                });

            var controller = new TechniqueController(mockService.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var modelResult = Assert.IsAssignableFrom<ICollection<TechniqueDTO>>(actionResult.Value);
            Assert.IsAssignableFrom<ICollection<TechniqueDTO>>(modelResult);
            Assert.Equal(3, modelResult.Count);
        }
        
         /**
         * PUT
         */
        [Fact]
        public async Task TestPutModelNull()
        {
            // Arrange
            var controller = new TechniqueController(null);

            // Act
            var response = await controller.Put(null);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(response);
        }

        [Fact]
        public async Task TestPutModelNamePropertyNull()
        {
            // Arrange
            var controller = new TechniqueController(null);

            // Act
            var response = await controller.Put(new TechniqueDTO());

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(response);
        }

        [Fact]
        public async Task TestPutUpdateThrows()
        {
            // Arrange
            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<TechniqueDTO>()))
                .ThrowsAsync(new EntityNotFoundException())
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);
            
            // Act
            var response = await controller.Put(new TechniqueDTO("test", "", "", "", "", ""));
            
            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(response);

            mockService
                .Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<TechniqueDTO>()), Times.Once());
        }

        [Fact]
        public async Task TestPut()
        {
            // Arrange
            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<TechniqueDTO>()))
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);
            
            // Act
            var response = await controller.Put(new TechniqueDTO("test", "", "", "", "", ""));
            
            // Assert
            Assert.IsAssignableFrom<NoContentResult>(response);

            mockService
                .Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<TechniqueDTO>()), Times.Once());
        }
        
        /**
         * Delete()
         */
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task TestDeleteEntityNameNull(string testName)
        {
            // Arrange
            var controller = new TechniqueController(null);
            
            // Act
            var response = await controller.Delete(testName);

            // Assert
            var actionResult = Assert.IsAssignableFrom<BadRequestResult>(response);
        }

        [Fact]
        public async Task TestDeleteEntityEntityNotFound()
        {
            // Arrange
            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ThrowsAsync(new EntityNotFoundException())
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);

            // Act
            var response = await controller.Delete("test");

            // Assert
            var actionResult = Assert.IsAssignableFrom<NotFoundObjectResult>(response);
            Assert.Equal($"Couldn't find a {nameof(Technique)} entity matching the name 'test'", actionResult.Value);

            mockService
                .Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task TestDelete()
        {
            // Arrange
            var mockService = new Mock<ITechniqueService>();
            mockService
                .Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .Verifiable();

            var controller = new TechniqueController(mockService.Object);

            // Act
            var response = await controller.Delete("test");

            // Assert
            var actionResult = Assert.IsAssignableFrom<NoContentResult>(response);

            mockService
                .Verify(x => x.DeleteAsync(It.IsAny<string>()), Times.Once());
        }
    }
}