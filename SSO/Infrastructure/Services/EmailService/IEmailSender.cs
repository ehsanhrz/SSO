namespace SSO.Infrastructure.Services.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
