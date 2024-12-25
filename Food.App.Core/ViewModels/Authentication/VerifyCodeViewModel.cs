using System.ComponentModel.DataAnnotations;

namespace Food.App.Core.ViewModels.Authentication;
public class VerifyCodeViewModel
{
    [StringLength(6)]
    public string Code { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(6)]

    public string Password { get; set; }

}
