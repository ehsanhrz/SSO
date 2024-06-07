
namespace SSO.Infrastructure.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
