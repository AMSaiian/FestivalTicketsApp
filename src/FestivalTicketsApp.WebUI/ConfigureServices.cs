using FestivalTicketsApp.Application.ClientService;
using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.TicketService;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FestivalTicketsApp.WebUI;

public static class ConfigureServices
{
    public static readonly string DefaultDbInstance = "LocalInstance2";
    
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(DefaultDbInstance);
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddExternalAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(options =>
            {
                options.Authority = "https://localhost:5001";
                
                options.CallbackPath = "/signin-oidc";
                options.SignedOutCallbackPath = "/signout-callback-oidc";
                
                options.ClientId = "FestivalTicketsApp";
                options.ClientSecret = "1C867CC4-F5EC-4DC2-81FE-615B14C242BE";
                
                options.ResponseType = OpenIdConnectResponseType.Code;
                
                options.SaveTokens = true;
                options.UsePkce = true;
                
                options.Scope.Add(OpenIdConnectScope.OfflineAccess);
                options.Scope.Add(OpenIdConnectScope.OpenId);
                options.Scope.Add("ClientInfo");
                
                options.Scope.Remove("profile");
                
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.MapAll();
            });
    
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IHostService, HostService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IClientService, ClientService>();

        return services;
    }
}