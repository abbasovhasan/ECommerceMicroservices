// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource {  Scopes = { "CatalogAPIFullAccess" } },
           // new ApiResource{ Scopes = { "BasketAPIFullAccess" } },
         //   new ApiResource{ Scopes = { "GatewayAPIFullAccess" } },
           // new ApiResource{ Scopes = { "OrderAPIFullAccess" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                        {
                            Name = "role",
                            DisplayName = "Roles",
                            Description = "Allow the service access to your user roles.",
                            UserClaims = new[] { "role" }
                        }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("CatalogAPIFullAccess","Catalog Api Application"),
                new ApiScope("BasketAPIFullAccess" ,"Basket API Full Access"),
                new ApiScope("GatewayAPIFullAccess","Gateway API Full Access"),
                new ApiScope("OrderAPIFullAccess","Order API Full Access"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "MVCApiClient",
                    ClientName = "ASP.NET Code Academy Core API",
                    ClientSecrets = { new Secret("codeacademy".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    

                    AllowedScopes = { "CatalogAPIFullAccess",
                    IdentityServerConstants.LocalApi.ScopeName,
                    }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
            };
    }
}