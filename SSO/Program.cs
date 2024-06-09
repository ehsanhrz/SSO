using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Quartz;
using SSO;
using SSO.Infrastructure.Database;
using SSO.Infrastructure.Database.Models;
using SSO.Utils;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallAllFeatures(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", builder =>
    {
        builder.WithOrigins("http://127.0.0.1:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("All", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>()
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
        options.UseEntityFrameworkCore().UseDbContext<AppDbContext>();
        options.UseQuartz();
    })
    .AddClient(options =>
    {
        options
            .UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableRedirectionEndpointPassthrough()
            .EnablePostLogoutRedirectionEndpointPassthrough();
        if (builder.Environment.IsDevelopment())
        {
            options
            .UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableRedirectionEndpointPassthrough()
            .EnablePostLogoutRedirectionEndpointPassthrough()
            .DisableTransportSecurityRequirement();

        }
        else
        {
            options
            .UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableRedirectionEndpointPassthrough()
            .EnablePostLogoutRedirectionEndpointPassthrough();
        }
        options.AllowAuthorizationCodeFlow().SetRedirectionEndpointUris("/callback");

        options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();

        options.UseSystemNetHttp().SetProductInformation(typeof(Program).Assembly);

       
    })
    .AddServer(options =>
    {
        options
            .SetAuthorizationEndpointUris("connect/authorize")
            .SetDeviceEndpointUris("connect/device")
            .SetIntrospectionEndpointUris("connect/introspect")
            .SetLogoutEndpointUris("connect/logout")
            .SetRevocationEndpointUris("connect/revoke")
            .SetTokenEndpointUris("connect/token")
            .SetUserinfoEndpointUris("connect/userinfo")
            .SetVerificationEndpointUris("connect/verify");

        options
            .AllowAuthorizationCodeFlow()
            .AllowDeviceCodeFlow()
            .AllowPasswordFlow()
            .AllowRefreshTokenFlow();

        options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile, OpenIddictConstants.Scopes.Roles);

        options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();

        options.RequireProofKeyForCodeExchange();

        

        if (builder.Environment.IsDevelopment())
        {
            options
            .UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserinfoEndpointPassthrough()
            .EnableVerificationEndpointPassthrough()
            .DisableTransportSecurityRequirement();
        }
        else
        {
            options
            .UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserinfoEndpointPassthrough()
            .EnableVerificationEndpointPassthrough();
        }
    })
    .AddValidation(options =>
    {
        options.AddAudiences("resource_server");
        options.UseLocalServer();
        options.UseAspNetCore();

        
    });



builder.Services.AddHostedService<Worker>();

var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/error");
app.UseRouting();

app.UseRequestLocalization(options =>
{
    options.AddSupportedCultures("en-US", "fr-FR");
    options.AddSupportedUICultures("en-US", "fr-FR");
    options.SetDefaultCulture("en-US");
});

//app.UseCors("AllowAngularClient");
app.UseCors("All");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}
app.Run();
