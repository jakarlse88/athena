using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.Controllers;
using Athena.Models.ViewModels;
using Athena.Services;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Athena.Test.ControllerTests
{
    public class TechniqueCategoryControllerTests
    {
        /**
         * All actions should have authorize attributes
         */
        [Fact]
        public void TestHasAuthorizeAttribute()
        {
            // Arrange
            var controller = new TechniqueCategoryController(null);

            // Act
            var methods = controller.GetType().GetDeclaredMethods();

            foreach (var method in methods)
            {
                // Assert
                Assert.Equal(typeof(AuthorizeAttribute), method.GetCustomAttributes(typeof(AuthorizeAttribute), true).First().GetType());
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
            var controller = new TechniqueCategoryController(null);

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

            var mockService = new Mock<ITechniqueCategoryService>();
            mockService
                .Setup(x => x.GetByNameAsync(testName))
                .ReturnsAsync(new TechniqueCategoryViewModel { Name = testName })
                .Verifiable();

            var controller = new TechniqueCategoryController(mockService.Object);

            // Act
            var result = await controller.Get(testName);

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var modelResult = Assert.IsAssignableFrom<TechniqueCategoryViewModel>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task TestGetEntityNotFound()
        {
            // Arrange
            const string testName = "Test";

            var mockService = new Mock<ITechniqueCategoryService>();
            mockService
                .Setup(x => x.GetByNameAsync(testName))
                .ReturnsAsync(new TechniqueCategoryViewModel())
                .Verifiable();

            var controller = new TechniqueCategoryController(mockService.Object);

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
            var controller = new TechniqueCategoryController(null);

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

            var mockService = new Mock<ITechniqueCategoryService>();
            mockService
                .Setup(ms => ms.CreateAsync(It.IsAny<TechniqueCategoryViewModel>()))
                .ReturnsAsync(new TechniqueCategoryViewModel { Name = testName })
                .Verifiable();

            var controller = new TechniqueCategoryController(mockService.Object);

            // Act
            var result = await controller.Post(new TechniqueCategoryViewModel());

            // Assert
            var actionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result);
            Assert.Equal("Get", actionResult.ActionName);
            var modelResult = Assert.IsAssignableFrom<TechniqueCategoryViewModel>(actionResult.Value);
            Assert.Equal(testName, modelResult.Name);

            mockService
                .Verify(ms => ms.CreateAsync(It.IsAny<TechniqueCategoryViewModel>()), Times.Once);
        }

        /**
         * Get() 
         */
        [Fact]
        public async Task TestGetNoResults()
        {
            // Arrange
            var mockService = new Mock<ITechniqueCategoryService>();
            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<TechniqueCategoryViewModel>());

            var controller = new TechniqueCategoryController(mockService.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var modelResult = Assert.IsAssignableFrom<ICollection<TechniqueCategoryViewModel>>(actionResult.Value);
            Assert.IsAssignableFrom<ICollection<TechniqueCategoryViewModel>>(modelResult);
            Assert.Empty(modelResult);
        }

        [Fact]
        public async Task TestGet()
        {
            // Arrange
            var mockService = new Mock<ITechniqueCategoryService>();
            mockService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<TechniqueCategoryViewModel>
                    { new TechniqueCategoryViewModel(), new TechniqueCategoryViewModel(), new TechniqueCategoryViewModel() });

            var controller = new TechniqueCategoryController(mockService.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = Assert.IsAssignableFrom<OkObjectResult>(response);
            var modelResult = Assert.IsAssignableFrom<ICollection<TechniqueCategoryViewModel>>(actionResult.Value);
            Assert.IsAssignableFrom<ICollection<TechniqueCategoryViewModel>>(modelResult);
            Assert.Equal(3, modelResult.Count);
        }
    }
}