using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Food.App.Core.Extensions;
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as FormFile;
        if (file != null)
        {
            if (file.Length > _maxFileSize)
            {
                double fileSize = (double)file.Length / (1024 * 1024);
                return new ValidationResult($"Maximum allowed size is  {_maxFileSize / 1024 / 1024} MB bytes your uploaded file is " +
                                    $" {Math.Round(fileSize, 2)} MB .");
            }
        }
        return ValidationResult.Success;

    }
}
