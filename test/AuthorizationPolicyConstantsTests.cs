using System;
using Athena.Infrastructure.Auth;
using Xunit;

namespace Athena.Test
{
    public class AuthorizationPolicyConstantsTests
    {
        [Theory]
        [InlineData("TechniqueReadPermission", "HasTechniqueReadPermission")]
        [InlineData("TechniqueWritePermission", "HasTechniqueWritePermission")]
        [InlineData("TechniqueUpdatePermission", "HasTechniqueUpdatePermission")]
        [InlineData("TechniqueDeletePermission", "HasTechniqueDeletePermission")]
        [InlineData("TechniqueCategoryReadPermission", "HasTechniqueCategoryReadPermission")]
        [InlineData("TechniqueCategoryWritePermission", "HasTechniqueCategoryWritePermission")]
        [InlineData("TechniqueCategoryUpdatePermission", "HasTechniqueCategoryUpdatePermission")]
        [InlineData("TechniqueCategoryDeletePermission", "HasTechniqueCategoryDeletePermission")]
        [InlineData("TechniqueTypeReadPermission", "HasTechniqueTypeReadPermission")]
        [InlineData("TechniqueTypeWritePermission", "HasTechniqueTypeWritePermission")]
        [InlineData("TechniqueTypeUpdatePermission", "HasTechniqueTypeUpdatePermission")]
        [InlineData("TechniqueTypeDeletePermission", "HasTechniqueTypeDeletePermission")]
        public void TestMapConfigKeyToPolicyName(string input, string expected)
        {
            // Arrange
            
            // Act
            var actual = AuthorizationPolicyConstants.MapConfigKeyToPolicyName(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestMapConfigKeyToPolicyNameInputNull()
        {
            // Arrange
            
            // Act
            static void Action() => AuthorizationPolicyConstants.MapConfigKeyToPolicyName(null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(Action);
            Assert.Equal("key", ex.ParamName);
        }

        [Fact]
        public void TestMapConfigKeyToPolicyNameInvalidInput()
        {
            // Arrange
            
            // Act
            static void Action() => AuthorizationPolicyConstants.MapConfigKeyToPolicyName("test");

            // Assert
            var ex = Assert.Throws<ArgumentException>(Action);
            Assert.Equal("No policy found for the key 'test'", ex.Message);            
        }
    }
}