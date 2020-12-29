using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LIS.Authentication
{
    public static class IdentityConfiguration
    {
        public static class WellknownClientId
        {
            public const string External = "external";
            public const string Admin = "admin";
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
           };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("LIS_Core_Api", "Api Core Scope"),
            new ApiScope("LIS_Gateway_Api", "Api Gateway Scope")
        };

        public static IEnumerable<ApiResource> GetApis(IDictionary<string, string[]> apiSecrets)
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "LIS_Admin_Resource",
                    DisplayName = "LIS Core service",
                    Description = "Access to core API",
                    ApiSecrets = apiSecrets["System"].Select(t => new Secret(t.Sha256())).ToList(),
                    Scopes = { "LIS_Core_Api", "LIS_Gateway_Api" }
                }
            };
        }

        public static IEnumerable<Client> GetClients(Dictionary<string, string[]> clientsUrl, Dictionary<string, string> clientSecrets)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = WellknownClientId.Admin,
                    ClientName = "Administration",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(clientSecrets["Admin"].Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Email,
                        "LIS_Gateway_Api",
                        "LIS_Core_Api"
                    }
                },
                new Client
                {
                    ClientId = WellknownClientId.External,
                    ClientName = "External Portal",
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(clientSecrets["External"].Sha256())
                    },
                    AllowedGrantTypes =
                    {
                        GrantType.ResourceOwnerPassword,
                        "external"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Reference,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris =  clientsUrl["External"].Map(t => t + "/oidc/callback"),
                    PostLogoutRedirectUris = clientsUrl["External"].Map(t => t + "/oidc/logout-callback"),
                    AllowedCorsOrigins = clientsUrl["External"],
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Email,
                        "LIS_Gateway_Api",
                        "LIS_Core_Api"
                    }
                }
            };
        }

    }

    internal static class UriHelper
    {
        internal static string[] Map(this string[] source, Func<string, string> selector)
        {
            return source.Select(selector).ToArray();
        }
    }
}
