using System.Text.RegularExpressions;
using Athena.Models.ViewModels;
using FluentValidation;

namespace Athena.Models.Validators
{
    public class TechniqueTypeValidator : AbstractValidator<TechniqueTypeViewModel>
    {
        public TechniqueTypeValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(new Regex(ValidationRegex.ValidAlphabetic));

            When(model => !string.IsNullOrWhiteSpace(model.NameHangeul), () =>
            {
                RuleFor(model => model.NameHangeul)
                    .MaximumLength(50)
                    .Matches(new Regex(ValidationRegex.ValidHangeul));
            });

            When(model => !string.IsNullOrWhiteSpace(model.NameHanja), () =>
            {
                RuleFor(model => model.NameHanja)
                    .MaximumLength(50)
                    .Matches(new Regex(ValidationRegex.ValidHanja));
            });
        }
    }
}