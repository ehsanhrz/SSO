namespace Microsoft.Extensions.DependencyInjection;


public class InstallerException : Exception
{
    private readonly string _installerName;
    public override string Message => $"Assembly '{_installerName}' doesn't include any Installer.";

    public InstallerException(string installerName)
    {
        _installerName = installerName;
    }
}


public interface IServiceCollectionInstaller
{
    int InstallerOrder { get; }
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}

public static class ServiceCollectionInstaller
{
    public static void InstallFromAssembly<T>(this IServiceCollection services, IConfiguration configuration)
    {
        // get all public classes that implement from IInstaller
        var installerItems = typeof(T).Assembly
                                    .GetExportedTypes()
                                        .Where(x => typeof(IServiceCollectionInstaller).IsAssignableFrom(x) &&
                                                    x is { IsAbstract: false, IsInterface: false })
                                        .Select(Activator.CreateInstance)
                                        .Cast<IServiceCollectionInstaller>()
                                        .ToList();

        // configure ServiceCollection for all founded classes 
        foreach (var installer in installerItems)
        {
            installer.ConfigureServices(services, configuration);
        }
    }

    public static void InstallAllFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in allAssemblies)
        {
            var installerItems = assembly
                .GetExportedTypes()
                .Where(x => typeof(IServiceCollectionInstaller).IsAssignableFrom(x) &&
                            x is { IsAbstract: false, IsInterface: false })
                .Select(Activator.CreateInstance)
                .Cast<IServiceCollectionInstaller>()
                .OrderBy(i => i.InstallerOrder)
                .ToList();

            foreach (var installer in installerItems)
            {
                installer.ConfigureServices(services, configuration);
                GC.Collect();
            }
        }

    }
    public static WebApplication BuildIt<T>(this WebApplicationBuilder builder)
    {
        builder.Services.InstallFromAssembly<T>(builder.Configuration);
        return builder.Build();
    }
    public static WebApplication BuildWithAllModules<T>(this WebApplicationBuilder builder)
    {
        builder.Services.InstallAllFeatures(builder.Configuration);
        return builder.Build();
    }


}
