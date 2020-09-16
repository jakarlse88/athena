using System.Threading.Tasks;
using Athena.Infrastructure.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Athena.Test.ValidationTests
{
    public class TechniqueValidatorTests
    {
        private readonly TechniqueValidator _validator;

        public TechniqueValidatorTests()
        {
            _validator = new TechniqueValidator();
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
        
        /**
         * NameHangeul 
         */
        
        [Fact]
        public void TestNameHangeulEmpty() =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.NameHangeul, "");
        
        [Theory]
        [InlineData("한굴")]
        [InlineData("한굴 한굴")]
        public void TestNameHangeulValidCharacters(string testString) =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.NameHangeul, testString);
        
        [Theory]
        [InlineData("This isn't hangeul")]
        [InlineData("This is both 한굴 and latin")]
        [InlineData("!!!")]
        public void TestNameHangeulInvalidCharacters(string testString) =>
            _validator.ShouldHaveValidationErrorFor(model => model.NameHangeul, testString);
        
        [Fact]
        public void TestNameHangeulTooLong() =>
            _validator.ShouldHaveValidationErrorFor(model => model.NameHangeul, "asdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåa");
        
        /**
         * NameHanja 
         */
        
        [Theory]
        [InlineData("This isn't hanzi")]
        [InlineData("This is both 한굴 and latin")]
        [InlineData("!!!")]
        public void TestNameHanjaInvalidCharacters(string testString) =>
            _validator.ShouldHaveValidationErrorFor(model => model.NameHanja, testString);
            
        [Theory]
        [InlineData("漢字")]
        [InlineData("漢字 漢字")]
        [InlineData("中文")]
        [InlineData("繁體字")]
        public void TestNameHanjaValidCharacters(string testString) =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.NameHanja, testString);
        
        [Fact]
        public void TestNameHanjaEmpty() =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.NameHanja, "");
        
        
        [Fact]
        public void TestNameHanjaTooLong() =>
            _validator.ShouldHaveValidationErrorFor(model => model.NameHanja, "asdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåa");
        
        /**
         * TechniqueCategoryId 
         */
        
        [Theory]
        [InlineData(0)]
        public void TestTechniqueCategoryIdEmpty(int testId) =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueCategoryId, testId);

        /**
         * TechniqueTypeId 
         */
        
        [Theory]
        [InlineData(0)]
        public void TestTechniqueTypeIdEmpty(int testId) =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueTypeId, testId);


    }
}