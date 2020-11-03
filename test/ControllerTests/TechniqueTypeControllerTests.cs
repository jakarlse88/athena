using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Controllers;
using Athena.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.Internal;
using System.Linq;
using Athena.Models.DTOs;

namespace Athena.Test.ControllerTests
{
    public class TechniqueTypeTests
    {
        /**
         * All actions should have authorize attributes
         */
        [Fact]
        public void TestHasAuthorizeAttribute()
        {
            // Arrange
            var controller = new TechniqueTypeController(null);

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
            var controller = new TechniqueTypeController(null);

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

            var mockService = new Mock<ITechniqueTypeService>();
            mockService
                .Setup(x => x.GetByNameAsync(testName))
                .ReturnsAsync(new TechniqueTypeDTO(testName, "", "", ""))
                .Verifiable();

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var result = await controller.Get(testName);

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var modelResult = Assert.IsAssignableFrom<TechniqueTypeDTO>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task TestGetEntityNotFound()
        {
            // Arrange
            const string testName = "Test";

            var mockService = new Mock<ITechniqueTypeService>();
            mockService
                .Setup(x => x.GetByNameAsync(testName))
                .ReturnsAsync(new TechniqueTypeDTO(testName, "", "", ""))
                .Verifiable();

            var controller = new TechniqueTypeController(mockService.Object);

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
            var controller = new TechniqueTypeController(null);

            // Act
            var result = await controller.Post(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task TestPostModelNotNull()
        {
            // Arrange
            const string testName = "test";

            var mockService = new Mock<ITechniqueTypeService>();
            mockService
                .Setup(ms => ms.CreateAsync(It.IsAny<TechniqueTypeDTO>()))
                .ReturnsAsync(new TechniqueTypeDTO(testName, "", "", ""))
                .Verifiable();

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var result = await controller.Post(new TechniqueTypeDTO("", "", "", ""));

            // Assert
            var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal("Get", actionResult.ActionName);
            var modelResult = Assert.IsAssignableFrom<TechniqueTypeDTO>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(ms => ms.CreateAsync(It.IsAny<TechniqueTypeDTO>()), Times.Once);
        }

        /**
         * Get() 
         */
        [Fact]
        public async Task TestGetNoResults()
        {
            // Arrange
            var mockService = new Mock<ITechniqueTypeService>();
            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<TechniqueTypeDTO>());

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var modelResult = Assert.IsAssignableFrom<ICollection<TechniqueTypeDTO>>(actionResult.Value);
            Assert.IsAssignableFrom<ICollection<TechniqueTypeDTO>>(modelResult);
            Assert.Empty(modelResult);
        }

        [Fact]
        public async Task TestGet()
        {
            // Arrange
            var mockService = new Mock<ITechniqueTypeService>();
            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<TechniqueTypeDTO>
                {
                    new TechniqueTypeDTO("", "", "", ""),
                    new TechniqueTypeDTO("", "", "", ""),
                    new TechniqueTypeDTO("", "", "", "")
                });

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var modelResult = Assert.IsAssignableFrom<ICollection<TechniqueTypeDTO>>(actionResult.Value);
            Assert.IsAssignableFrom<ICollection<TechniqueTypeDTO>>(modelResult);
            Assert.Equal(3, modelResult.Count);
        }
    }
}