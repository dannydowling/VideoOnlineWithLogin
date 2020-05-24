using IdentityModel;
using IdentityServer4.Models;
using PreFlightAI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreFlight.AI.IDP
{
    public class Config
    {
        private readonly Employee employee; //the employee for access credential
        private readonly typedUser user; //the user for access credential

        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("jobcategory", new [] {  "jobCategory"}),
                new IdentityResource("email", new [] {"email"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("PreFlight.AI.API",
                    "Internal Server Communication",
                    new [] {"".Sha512() //The Api secret key to register the API on OpenID Connect
                    }
                    )
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "IdentityClient",
                    ClientName = "PreFlight Mangement",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = {new Secret("".Sha512())}, //put in the client secret key here
                    RedirectUris = {"https://localhost:43366/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:43366/signout-callback-oidc"},
                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "email", "PreFlight.AI.API" }

                }
            };
    }
}
