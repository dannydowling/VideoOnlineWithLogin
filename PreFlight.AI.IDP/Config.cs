using IdentityModel;
using IdentityServer4.Models;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Employee;
using PreFlightAI.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace PreFlight.AI.IDP
{
    public class Config
    {
        
        public static IEnumerable<IdentityResource> IDP =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("AUTH_Employee", new [] {"Owner", "Senior Manager", "Manager", "IT Lead", "IT Worker" }),
                new IdentityResource("DEAUTH_Employee", new [] {"Worker"}),
                new IdentityResource("AUTH_User", new [] {"Verified"}),
                new IdentityResource("DEAUTH_User", new [] {"Guest", "Visitor"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("internalServerCommunication",
                    "Internal Server Communication",
                    new [] {"IT_DANNY".Sha512() //The Api secret key to register the API on OpenID Connect
                    })
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               
                   new Client
                {
                    ClientId = "APIClient",
                    ClientName = "PreFlight Internal",
                    AllowedGrantTypes = GrantTypes.Code, //long lived access, Tokens from token endpoint
                    RequirePkce = true,

                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AbsoluteRefreshTokenLifetime = 86400, //Allow pre-auth for 1 day

                    ClientSecrets = {new Secret("IT_DANNY".Sha512())}, //put in the client secret key here
                    
                       RedirectUris = {"https://localhost:44301/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:44301/signout-callback-oidc"},

                       AllowOfflineAccess = true,
                            RequireConsent = false,

                       AllowedScopes = {"internalServerCommunication" }

                },

                
                new Client
                {
                    ClientId = "EmployeeClient",
                    ClientName = "PreFlight Management",
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,

                     RefreshTokenExpiration = TokenExpiration.Sliding,
                    AbsoluteRefreshTokenLifetime = 86400, //Allow pre-auth for 1 day

                    ClientSecrets = {new Secret("IT_DANNY".Sha512())}, //put in the client secret key here

                    RedirectUris = {"https://localhost:44301/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:44301/signout-callback-oidc"},
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                    AllowedScopes = {"openid", "email", "AUTH_Employee" }

                },

                new Client
                {
                    ClientId = "UserClient",
                    ClientName = "PreFlight Users",
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,

                     RefreshTokenExpiration = TokenExpiration.Sliding,
                        AbsoluteRefreshTokenLifetime = 86400, //Allow pre-auth for 1 day

                    ClientSecrets = {new Secret("IT_DANNY".Sha256())}, //put in the client secret key here
                    RedirectUris = {"https://localhost:44301/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:44301/signout-callback-oidc"},
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                    AllowedScopes = {"openid", "email", "DEAUTH_Employee", "AUTH_User" , "DEAUTH_User" }

                }
            };
    }
}
