using Athena.Models.Validators;
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

        [Theory]
        [InlineData("Jireugi")]
        [InlineData("Arae-makgi")]
        public void TestNameValidationValid(string testString) =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.Name, testString);
        
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
         * TechniqueCategoryName 
         */
                
        [Fact]
        public void TestTechniqueCategoryNameValidationEmpty() =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueCategoryName, "");

        [Fact]
        public void TestTechniqueCategoryNameTooLong() =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueCategoryName, "asdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåa");

        [Fact]
        public void TestTechniqueCategoryNameValidationValid() =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.TechniqueCategoryName, "Jireugi");
        
        [Fact]
        public void TestTechniqueCategoryNameInvalidCharacters() =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueCategoryName, "Jireugi!");

        [Theory]
        [InlineData("한굴")]
        [InlineData("This is both 한굴 and latin")]
        [InlineData("漢字")]
        [InlineData("This is  both 漢字 and latin")]
        public void TestTechniqueCategoryNameIllegalScripts(string testString) =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueCategoryName, testString);
        
        /**
         * TechniqueTypeName
         */
                
        [Fact]
        public void TestTechniqueTypeNameValidationEmpty() =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueTypeName, "");

        [Fact]
        public void TestTechniqueTypeNameTooLong() =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueTypeName, "asdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåasdfghjkløæåa");

        [Fact]
        public void TestTechniqueTypeNameValidationValid() =>
            _validator.ShouldNotHaveValidationErrorFor(model => model.TechniqueTypeName, "Jireugi");
        
        [Fact]
        public void TestTechniqueTypeNameInvalidCharacters() =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueTypeName, "Jireugi!");

        [Theory]
        [InlineData("한굴")]
        [InlineData("This is both 한굴 and latin")]
        [InlineData("漢字")]
        [InlineData("This is  both 漢字 and latin")]
        public void TestTechniqueTypeNameIllegalScripts(string testString) =>
            _validator.ShouldHaveValidationErrorFor(model => model.TechniqueTypeName, testString);


    }
}