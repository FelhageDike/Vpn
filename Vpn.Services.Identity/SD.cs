using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Vpn.Services.Identity;

public static class SD
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("Vpn", "Vpn Server"),
            new ApiScope("read", "read your data"),
            new ApiScope("write", "write your data"),
            new ApiScope("Delete", "Delete your data"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
                ClientId = "Vpn",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7273/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7273/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Phone,
                    IdentityServerConstants.StandardScopes.Email,
                    "Vpn"
                }
            }
        };
}