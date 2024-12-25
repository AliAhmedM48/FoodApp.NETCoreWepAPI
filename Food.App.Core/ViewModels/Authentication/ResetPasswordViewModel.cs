using System.ComponentModel.DataAnnotations;

namespace Food.App.Core.ViewModels.Authentication;
public class ResetPasswordViewModel
{
    [Required]
    public string OldPassword { get; set; } = string.Empty;
    [Required]

    public string NewPassword { get; set; } = string.Empty;
    [Required]

    public string ConfirmPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
