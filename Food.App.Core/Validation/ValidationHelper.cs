using FluentValidation;
using FluentValidation.Results;

namespace Food.App.Core.Validation;
public static class ValidationHelper
{
    public static void ValidateArgumentsNullOrEmpty(params object[] arguments)
    {
        foreach (var argument in arguments)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(nameof(argument), "Argument cannot be null.");
            }

            if (argument is string str && string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("String argument cannot be null, empty, or whitespace.", nameof(argument));
            }

            if (argument is IEnumerable<object> enumerable && !enumerable.Any())
            {
                throw new ArgumentException("Collection cannot be empty or contain null values.", nameof(argument));
            }
        }
    }

    public static async Task<List<ValidationError>?> ValidateViewModelAsync<TViewModel>(IValidator<TViewModel> validator, object viewModelToValidate)
    {
        List<ValidationError>? validationErrors = new List<ValidationError>(); ;
        ValidationResult? validationResult = null;

        if (viewModelToValidate is IEnumerable<TViewModel> viewModelCollection)
        {
            foreach (var viewModel in viewModelCollection)
            {
                validationResult = await validator.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                {
                    validationErrors.AddRange(
                        validationResult.Errors
                        .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)).ToList());
                }
            }
        }
        else
        {
            validationResult = await validator.ValidateAsync((TViewModel)viewModelToValidate);

            if (!validationResult.IsValid)
            {
                validationErrors =
                    validationResult.Errors
                   .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage)).ToList();
            }
        }

        if (!validationErrors.Any()) return null;

        return validationErrors;
    }
}

public class ValidationError
{
    public string PropertyName { get; private set; }
    public string ErrorMessage { get; private set; }

    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
}