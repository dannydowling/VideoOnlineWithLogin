// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

#nullable enable

namespace PreFlight.AI.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles","Your role(s)",  new List<string> {"role"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                 new ApiResource(
                     "IDPClient",
                    "Internal Server Communication",
                    new List<string>() {"IDPContext"})
                    {
                        ApiSecrets = {new Secret("IT_DANNY".Sha512()) //The Api secret key to register the API on OpenID Connect
                    }
                 }
            };



        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // code flow client
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120,
                    AllowedCorsOrigins = { "http://localhost:44336" },
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientId = "Client",
                    ClientName = "PreFlight",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials, //long lived access, Tokens from token endpoint
                    RequirePkce = true,
                    //Where to redirect to after login
                    RedirectUris = { "https://localhost:44336/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:44336/signout-callback-oidc" },


                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "roles",
                        "IDPClient"},
                    ClientSecrets = {new Secret("IT_DANNY".Sha512())}, //put in the client secret key here 
                },


                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientName = "MVC Client",

                //    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                //    RequirePkce = true,
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    RefreshTokenExpiration = TokenExpiration.Sliding,
                //    AbsoluteRefreshTokenLifetime = 86400, //Allow pre-auth for 1 day

                //    RedirectUris = { "https://localhost:44336/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:44336/signout-oidc",
                //    PostLogoutRedirectUris = { "https://localhost:44336/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = {
                                                           
                //    }
                //}
            };
    }
}
