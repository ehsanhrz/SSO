using Microsoft.AspNetCore.Identity;
using SSO.Infrastructure.Database;
using SSO.Infrastructure.Database.Models;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);


builder.Services.InstallAllFeatures(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMvc();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddQuartz(options =>
{
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});


builder.Services.AddOpenIddict()

            
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                       .UseDbContext<AppDbContext>();
                options.UseQuartz();
            })

            
            .AddClient(options =>
            {
                
                options.AllowAuthorizationCodeFlow();
                
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();
                
                options.UseAspNetCore()
                       .EnableStatusCodePagesIntegration()
                       .EnableRedirectionEndpointPassthrough();

                options.UseSystemNetHttp()
                       .SetProductInformation(typeof(Program).Assembly);

                
            })

            
            .AddServer(options =>
            {
                
                options.SetAuthorizationEndpointUris("connect/authorize")
                       .SetDeviceEndpointUris("connect/device")
                       .SetIntrospectionEndpointUris("connect/introspect")
                       .SetLogoutEndpointUris("connect/logout")
                       .SetRevocationEndpointUris("connect/revoke")
                       .SetTokenEndpointUris("connect/token")
                       .SetUserinfoEndpointUris("connect/userinfo")
                       .SetVerificationEndpointUris("connect/verify");

                
                options.AllowAuthorizationCodeFlow()
                       .AllowDeviceCodeFlow()
                       .AllowPasswordFlow()
                       .AllowRefreshTokenFlow();

                
                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                
                options.RequireProofKeyForCodeExchange();

                
                options.UseAspNetCore()
                       .EnableStatusCodePagesIntegration()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableLogoutEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .EnableVerificationEndpointPassthrough();

                
            })

            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Configure the audience accepted by this resource server.
                // The value MUST match the audience associated with the
                // "demo_api" scope, which is used by ResourceController.
                options.AddAudiences("resource_server");

                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                

                // Register the ASP.NET Core host.
                options.UseAspNetCore();

                

            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
