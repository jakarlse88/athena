using System.Threading.Tasks;
using Athena.Controllers;
using Athena.Services;
using Athena.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Athena.Test.ControllerTests
{
    public class TechniqueControllerTests
    {
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
                .Setup(ms => ms.CreateAsync(It.IsAny<TechniqueViewModel>()))
                .ReturnsAsync(new TechniqueViewModel { Name = testName })
                .Verifiable();
            
            var controller = new TechniqueController(mockService.Object);
            
            // Act
            var result = await controller.Post(new TechniqueViewModel());

            // Assert
            var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal("Get", actionResult.ActionName);
            var modelResult = Assert.IsAssignableFrom<TechniqueViewModel>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);
            
            mockService
                .Verify(ms => ms.CreateAsync(It.IsAny<TechniqueViewModel>()), Times.Once);
        }

    }
}