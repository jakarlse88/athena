using Athena.Infrastructure.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Athena.Test.ValidationTests
{
    public class TechniqueTypeValidatorTests
    {
        private readonly TechniqueTypeValidator _validator;

        public TechniqueTypeValidatorTests()
        {
            _validator = new TechniqueTypeValidator();
        }
        
        /**
         * Name 
         */
        
        [Fact]
        public void TestNameValidationEmpty() =>
            _validator.ShouldHaveValidationErrorFor(model => model.Name, "");

        [Fact]
        public void TestNameTooLong() =>
            _validator.ShouldHaveValidationErrorFor(model => model.Name, "asdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåa");

        [Fact]
        public void TestNameValidationValid() =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.Name, "Jireugi");
        
        [Fact]
        public void TestNameInvalidCharacters() =>
            _validator.ShouldHaveValidationErrorFor(model => model.Name, "Jireugi!");

        [Theory]
        [InlineData("한굴")]
        [InlineData("This is both 한굴 and latin")]
        [InlineData("漢字")]
        [InlineData("This is  both 漢字 and latin")]
        public void TestNameIllegalScripts(string testString) =>
            _validator.ShouldHaveValidationErrorFor(model => model.Name, testString);
    }
}