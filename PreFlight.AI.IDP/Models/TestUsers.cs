// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace PreFlightAI.IDP
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{
                SubjectId = "Danny", Username = "Danny", Password = "IT_DANNY",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Danny Dowling"),
                    new Claim(JwtClaimTypes.GivenName, "Danny"),
                    new Claim(JwtClaimTypes.FamilyName, "Dowling"),
                    new Claim(JwtClaimTypes.Email, "Danny.Dowling@gmail.com"),
                    new Claim("AUTH_Employee", "Owner")
                }
            }
            
        };
    }
}