
namespace SSO.Infrastructure.Services.SmsService
{
    public class SmsSender : ISmsSender
    {
        public Task SendSmsAsync(string number, string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
