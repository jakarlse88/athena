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
                .Matches(ValidationRegex.ValidAlphabetic);

            RuleFor(model => model.TechniqueTypeName)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(ValidationRegex.ValidAlphabetic);

            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(ValidationRegex.ValidAlphabetic);

            When(model => !string.IsNullOrWhiteSpace(model.NameHangeul), () =>
            {
                RuleFor(model => model.NameHangeul)
                    .MaximumLength(50)
                    .Matches(ValidationRegex.ValidHangeul);
            });

            When(model => !string.IsNullOrWhiteSpace(model.NameHanja), () =>
            {
                RuleFor(model => model.NameHanja)
                    .MaximumLength(50)
                    .Matches(ValidationRegex.ValidHanja);    
            });
        }
    }
}

