namespace SSO.Infrastructure.Services.SmsService
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
