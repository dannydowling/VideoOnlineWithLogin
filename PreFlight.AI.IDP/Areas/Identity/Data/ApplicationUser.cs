using IdentityModel;
using IdentityServer4.Stores.Serialization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PreFlight.IDP.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Add profile data for application users by adding properties to the ApplicationUser class


        public static List<ApplicationUser> Users = new List<ApplicationUser>
        {
            new ApplicationUser()

            {
                Id = "Danny",
                UserName = "Danny",
                Email = "Danny.Dowling@gmail.com"

            } 
        };
        
    }
}
