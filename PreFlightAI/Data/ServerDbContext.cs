using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;

namespace PreFlight.AI.Server.Services.SQL
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options)
            : base(options)
        {            
        }        

        public DbSet<Location> Locations { get; set; }       
        public DbSet<Weather> Weathers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 1, Name = "Juneau" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 2, Name = "Seattle" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 3, Name = "Fairbanks" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 4, Name = "Anchorage" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 5, Name = "Ketchikan" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 6, Name = "Sitka" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 7, Name = "Wrangell" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 8, Name = "Petersburg" });            

   
            
            modelBuilder.Entity<Weather>().HasData(new Weather
            {
                weatherID = 1,
                WeightValue = 6.70,
                AirPressure = 1,
                Temperature = 60
            
            });
        }
    }
}
