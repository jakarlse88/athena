using System.Threading.Tasks;
using Athena.Controllers;
using Athena.Services;
using Athena.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Athena.Test.ControllerTests
{
    public class TechniqueTypeTests
    {
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