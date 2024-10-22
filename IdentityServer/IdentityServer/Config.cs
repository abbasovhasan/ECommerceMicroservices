// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Shared.Resources;
using System;
using System.Collections.Generic;
using static Shared.Consts.IdentityServerConst;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource( CATALOGAPIFULLACCESS ) {  Scopes = { "CatalogAPIFullAccess" } },
            new ApiResource( IMAGEAPIFULLACCESS) { Scopes = { "ImageAPIFullAccess" } },
            new ApiResource( BASKETAPIFULLACCESS) { Scopes = { "BasketAPIFullAccess" } },
            new ApiResource( DISCOUNTAPIFULLACCESS) { Scopes = { "DiscountAPIFullAccess" } },
            new ApiResource( PAYMENTAPIFULLACCESS) { Scopes = { "PaymentAPIFullAccess" } },
            new ApiResource( GATEWAYAPIFULLACCESS) { Scopes = { "GatewayAPIFullAccess" } },
            new ApiResource( ORDERAPIFULLACCESS) { Scopes = { "OrderAPIFullAccess" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResources.Email(),
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
                new ApiScope("ImageAPIFullAccess"  ,"Image API Full Access"),
                new ApiScope("BasketAPIFullAccess" ,"Basket API Full Access"),
                new ApiScope("DiscountAPIFullAccess","Discount API Full Access"),
                new ApiScope("PaymentAPIFullAccess","Payment API Full Access"),
                new ApiScope("GatewayAPIFullAccess","Gateway API Full Access"),
                new ApiScope("OrderAPIFullAccess","Order API Full Access"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "MVCApiClient",
                    ClientName = "ASP.NET CodeAcademy Core API",
                    ClientSecrets = { new Secret("codeacademy".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                         IdentityServerResources.CATALOGAPIFULLACCESS,
                        "ImageAPIFullAccess",
                        "GatewayAPIFullAccess",
                        IdentityServerConstants.LocalApi.ScopeName,
                    }
                },

                // user password flow client
                new Client
                {
                    ClientId = "MVCUserApiClient",
                    ClientName = "MVC User Api Client",
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret("codeacademy".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {
                        "role",
                        "BasketAPIFullAccess",
                        //"DiscountAPIFullAccess",
                        //"PaymentAPIFullAccess",
                        //"GatewayAPIFullAccess",
                        "OrderAPIFullAccess",
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,  // Profile eklenmeli
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    AccessTokenLifetime = 1 * 60 * 60, // 1 saat
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds, // 60 gün
                    RefreshTokenUsage = TokenUsage.ReUse,
                }
            };
    }
}