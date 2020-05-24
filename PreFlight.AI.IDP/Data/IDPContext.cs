using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PreFlight.AI.IDP.Data
{
    public class IDPContext : IdentityDbContext
    {
        public IDPContext(DbContextOptions<IDPContext> options)
            : base(options)
        {
        }
    }
}
