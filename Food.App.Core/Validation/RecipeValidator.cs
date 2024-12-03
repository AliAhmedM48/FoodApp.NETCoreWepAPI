using FluentValidation;
using Food.App.Core.Entities;

namespace Food.App.Core.Validation;
public class RecipeValidator : AbstractValidator<Recipe>
{
    public RecipeValidator()
    {
        //RuleFor(c => c.Name)
        // .NotEmpty().WithMessage("Name is required.")
        // .MaximumLength(250).WithMessage("Name must not exceed 250 characters.");
    }
}
