using System.Text.RegularExpressions;
using Athena.Models.ViewModels;
using FluentValidation;

namespace Athena.Models.Validators
{
    public class TechniqueValidator : AbstractValidator<TechniqueViewModel>
    {
        public TechniqueValidator()
        {
            RuleFor(model => model.TechniqueCategoryName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(new Regex(@"^[a-zA-Z ]*$"));

            RuleFor(model => model.TechniqueTypeName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(new Regex(@"^[a-zA-Z ]*$"));

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(new Regex(@"^[a-zA-Z ]*$"));

            When(model => !string.IsNullOrWhiteSpace(model.NameHangeul), () =>
            {
                RuleFor(model => model.NameHangeul)
                    .MaximumLength(50)
                    .Matches(new Regex(@"^[\p{IsHangulSyllables} ]*$"));
            });

            When(model => !string.IsNullOrWhiteSpace(model.NameHanja), () =>
            {
                RuleFor(model => model.NameHanja)
                    .MaximumLength(50)
                    .Matches(new Regex(@"^[\p{IsCJKUnifiedIdeographs} ]*$"));    
            });
        }
    }
}

