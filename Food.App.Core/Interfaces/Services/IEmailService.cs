namespace Food.App.Core.Interfaces.Services;
public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
