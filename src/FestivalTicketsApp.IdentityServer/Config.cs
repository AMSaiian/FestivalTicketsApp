using Duende.IdentityServer.Models;
using IdentityModel;

namespace FestivalTicketsApp.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(
                name: "ClientInfo",
                userClaims: 
                [
                    JwtClaimTypes.Subject, 
                    JwtClaimTypes.GivenName, 
                    JwtClaimTypes.FamilyName, 
                    JwtClaimTypes.Email, 
                    JwtClaimTypes.Actor 
                ])
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "FestivalTicketsApp",
                ClientName = "Festival Tickets App",
                ClientSecrets = { new Secret("1C867CC4-F5EC-4DC2-81FE-615B14C242BE".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7129/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7129/signout-callback-oidc" },
                
                RequirePkce = true,
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "ClientInfo" }
            },
        };
}