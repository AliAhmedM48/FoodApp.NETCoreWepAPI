using Food.App.Core.FileSetting;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Food.App.Core.Extensions;
public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string _allowedExtensions;
    public AllowedExtensionsAttribute(string allowedExtesion)
    {
        _allowedExtensions = allowedExtesion;
    }
    protected override ValidationResult? IsValid (object? value, ValidationContext validationContext)
    {

        var file = value as FormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            var isAllowed = FileSettings.AllowedExtensions.Contains(extension);
            if (!isAllowed)
            {
                return new ValidationResult($"Only {_allowedExtensions} are allowed");

            }

        }
        return ValidationResult.Success;


    }
}
