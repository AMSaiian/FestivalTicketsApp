using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using FestivalTicketsApp.IdentityServer.Data;
using FestivalTicketsApp.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FestivalTicketsApp.IdentityServer;

public static class SeedData
{
    public static async Task EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        await SeedUsers(scope);
        await SeedConfiguration(scope);
    }

    private static async Task SeedUsers(IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var alice = await userManager.FindByNameAsync("alice");
        if (alice == null)
        {
            alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
                ClientId = 1
            };
            var result = await userManager.CreateAsync(alice, "Pass123$");
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = await userManager.AddClaimsAsync(alice, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Alice Smith"),
                new Claim(JwtClaimTypes.GivenName, "Alice"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.Actor, "1"),
            });
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }

        var bob = await userManager.FindByNameAsync("bob");
        if (bob == null)
        {
            bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true,
                ClientId = 1
            };
            var result = await userManager.CreateAsync(bob, "Pass123$");
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = await userManager.AddClaimsAsync(bob, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Bob Smith"),
                new Claim(JwtClaimTypes.GivenName, "Bob"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                new Claim(JwtClaimTypes.Actor, "2")
            });
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("bob created");
        }
        else
        {
            Log.Debug("bob already exists");
        } 
    }

    private static async Task SeedConfiguration(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        
        if (!await context.Clients.AnyAsync())
        {
            foreach (var client in Config.Clients)
            {
                await context.Clients.AddAsync(client.ToEntity());
            }
            await context.SaveChangesAsync();
        }

        if (!await context.IdentityResources.AnyAsync())
        {
            foreach (var resource in Config.IdentityResources)
            {
                await context.IdentityResources.AddAsync(resource.ToEntity());
            }
            await context.SaveChangesAsync();
        }

        if (!await context.ApiScopes.AnyAsync())
        {
            foreach (var resource in Config.ApiScopes)
            {
                await context.ApiScopes.AddAsync(resource.ToEntity());
            }
            await context.SaveChangesAsync();
        }
    }
}