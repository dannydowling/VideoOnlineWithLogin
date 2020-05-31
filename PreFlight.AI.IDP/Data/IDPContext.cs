using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PreFlightAI.Shared.Employee;
using PreFlightAI.Shared.Users;

namespace PreFlight.AI.Server.Services.SQL
{
    public class IDPContext : IdentityDbContext
    {
        public IDPContext(DbContextOptions<IDPContext> options)
            : base(options)
        {
        }

        

      
    }
}
