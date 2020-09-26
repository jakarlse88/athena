using System.Threading.Tasks;
using Athena.Controllers;
using Athena.Models.ViewModels;
using Athena.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Athena.Test.ControllerTests
{
    public class TechniqueTypeTests
    {
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
                .ReturnsAsync(new TechniqueTypeViewModel { Name = testName })
                .Verifiable();

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var result = await controller.Get(testName);

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var modelResult = Assert.IsAssignableFrom<TechniqueTypeViewModel>(actionResult.Value);
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
                .ReturnsAsync(new TechniqueTypeViewModel())
                .Verifiable();

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var result = await controller.Get("Incorrect name");

            // Assert
            var actionResult = Assert.IsAssignableFrom<NotFoundResult>(result);

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
                .Setup(ms => ms.CreateAsync(It.IsAny<TechniqueTypeViewModel>()))
                .ReturnsAsync(new TechniqueTypeViewModel { Name = testName })
                .Verifiable();

            var controller = new TechniqueTypeController(mockService.Object);

            // Act
            var result = await controller.Post(new TechniqueTypeViewModel());

            // Assert
            var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal("Get", actionResult.ActionName);
            var modelResult = Assert.IsAssignableFrom<TechniqueTypeViewModel>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(ms => ms.CreateAsync(It.IsAny<TechniqueTypeViewModel>()), Times.Once);
        }
    }
}