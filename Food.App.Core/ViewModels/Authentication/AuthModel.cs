using System.Text.Json.Serialization;

namespace Food.App.Core.ViewModels.Authentication;
public class AuthModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsAuthenticated { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
}

