// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

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
                new IdentityResource("IDPClient", new [] {"IDPContext"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                 new ApiResource("IDPClient",
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
                    ClientId = "IDPClient",
                    ClientName = "PreFlight Internal",
                    AllowedGrantTypes = GrantTypes.Code, //long lived access, Tokens from token endpoint
                    
                    //Where to redirect to after login
                    RedirectUris = { "https://localhost:5001/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                    ClientSecrets = {new Secret("IT_DANNY".Sha512())}, //put in the client secret key here              
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "IDPClient" }
                },


                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AbsoluteRefreshTokenLifetime = 86400, //Allow pre-auth for 1 day

                    RedirectUris = { "https://localhost:44336/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44336/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44336/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email                                     
                    }
                }
            };
    }
}
