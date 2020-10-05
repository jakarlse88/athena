using Athena.Models.ViewModels;
using FluentValidation;

namespace Athena.Models.Validators
{
    public class TechniqueCategoryValidator : AbstractValidator<TechniqueCategoryViewModel>
    {
        public TechniqueCategoryValidator()
        {
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