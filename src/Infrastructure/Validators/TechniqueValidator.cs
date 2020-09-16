using System.Text.RegularExpressions;
using Athena.ViewModels;
using FluentValidation;

namespace Athena.Infrastructure.Validators
{
    public class TechniqueValidator : AbstractValidator<TechniqueViewModel>
    {
        public TechniqueValidator()
        {
            RuleFor(model => model.TechniqueCategoryId)
                .NotEmpty();

            RuleFor(model => model.TechniqueTypeId)
                .NotEmpty();

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(new Regex(@"^[a-zA-Z ]*$"));

            RuleFor(model => model.NameHangeul)
                .MaximumLength(50)
                .Matches(new Regex(@"^[\p{IsHangulSyllables} ]*$"));
            
            RuleFor(model => model.NameHanja)
                .MaximumLength(50)
                .Matches(new Regex(@"^[\p{IsCJKUnifiedIdeographs} ]*$"));
        }
    }
}

