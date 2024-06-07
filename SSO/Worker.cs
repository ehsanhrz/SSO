using OpenIddict.Abstractions;
using SSO.Infrastructure.Database;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SSO
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            await RegisterApplicationsAsync(scope.ServiceProvider);
            await RegisterScopesAsync(scope.ServiceProvider);
        }

        private static async Task RegisterApplicationsAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("angular_spa") is null)
            {
                await manager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = "angular_spa",
                        DisplayName = "Angular SPA",
                        RedirectUris = { new Uri("http://localhost:4200/callback") },
                        PostLogoutRedirectUris = { new Uri("http://localhost:4200") },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles
                        },
                        Requirements = { Requirements.Features.ProofKeyForCodeExchange }
                    }
                );
            }

            if (await manager.FindByClientIdAsync("aspnetcore_api") is null)
            {
                await manager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = "aspnetcore_api",
                        ClientSecret = "api_secret",
                        DisplayName = "ASP.NET Core API",
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection,
                            Permissions.GrantTypes.ClientCredentials,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles
                        }
                    }
                );
            }
        }

        private static async Task RegisterScopesAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("api_scope") is null)
            {
                await manager.CreateAsync(
                    new OpenIddictScopeDescriptor
                    {
                        Name = "api_scope",
                        DisplayName = "API Access",
                        Resources = { "resource_server" }
                    }
                );
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
