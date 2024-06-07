
using Microsoft.EntityFrameworkCore;
using SSO.Options;

namespace SSO.Infrastructure.Database
{
    public class DatabaseInstaller : IServiceCollectionInstaller
    {
        public int InstallerOrder => 0;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOption>(
                   configuration.GetSection(new ConnectionStringOption().OptionName));

            var connectionStrings = configuration.GetSection(new ConnectionStringOption().OptionName).Get<ConnectionStringOption>() ?? throw new Exception();

            services.AddDbContext<AppDbContext>(o => {
                o.UseNpgsql(connectionStrings.DefaultConnection);

                o.UseOpenIddict();
            });
        }
    }
}
