using System.Text.RegularExpressions;
using Athena.Models.ViewModels;
using FluentValidation;

namespace Athena.Infrastructure.Validators
{
    public class TechniqueCategoryValidator : AbstractValidator<TechniqueCategoryViewModel>
    {
        public TechniqueCategoryValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(new Regex(@"^[a-zA-Z ]*$"));
        }
    }
}