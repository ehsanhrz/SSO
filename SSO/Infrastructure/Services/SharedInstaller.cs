

using SSO.Infrastructure.Services.EmailService;
using SSO.Infrastructure.Services.SmsService;

namespace SSO.Infrastructure.Services
{
    public class SharedInstaller : IServiceCollectionInstaller
    {
        public int InstallerOrder => 1;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<ISmsSender, SmsSender>();
        }
    }
}
