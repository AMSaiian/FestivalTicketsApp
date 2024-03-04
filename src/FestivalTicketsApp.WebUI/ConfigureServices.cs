using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.WebUI;

public static class ConfigureServices
{
    public static readonly string DefaultDbInstance = "LocalInstance";
    
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(DefaultDbInstance);
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<Client>(options =>
            {
                options.User.RequireUniqueEmail = true;
                
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IHostService, HostService>();

        return services;
    }
}