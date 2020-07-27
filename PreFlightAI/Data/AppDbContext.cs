using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PreFlight.AI.Server.Data;
using PreFlightAI.Shared;

namespace PreFlightAI.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserModel> typedUsers { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
          
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 1, Name = "Juneau" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 2, Name = "Sitka" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 3, Name = "Ketchikan" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 4, Name = "Petersburg" });
            modelBuilder.Entity<Location>().HasData(new Location { LocationId = 5, Name = "Wrangell" });

            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 1, JobCategoryName = "Guest" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 2, JobCategoryName = "Visitor" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 3, JobCategoryName = "Verified" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 4, JobCategoryName = "Worker" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 5, JobCategoryName = "IT Worker" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 6, JobCategoryName = "IT Lead" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 7, JobCategoryName = "Manager" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 8, JobCategoryName = "Senior Manager" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 9, JobCategoryName = "Owner" });

            Location juneau = new Location { LocationId = 1, City = "Juneau", Country = Country.America, Name = "Juneau", Street = "none", Zip = "99801" };

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                JobCategoryId = 9,
                Password = "Password",
                BirthDate = new DateTime(1979, 1, 16),

                Email = "danny.dowling@gmail.com",
                FirstName = "Danny",
                LastName = "Dowling",
                PhoneNumber = "324777888773",
                employeeLocations = new List<Location>() { juneau },
                ExitDate = null,
                JoinedDate = new DateTime(2019, 3, 1)
            }) ;

         

            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                UserID = 1,
                Email = "danny.dowling@gmail.com",
                FirstName = "Danny",
                LastName = "Dowling",
                Password = "password",
                Comment = "Using Fake Address and Phone number here",
                ExitDate = null,
                JoinedDate = new DateTime(2019, 3, 1)
                
            });

            modelBuilder.Entity<Weather>().HasAlternateKey(w => w.userId.Weathers);
            modelBuilder.Entity<UserModel>().HasMany(e => e.Weathers);
            modelBuilder.Entity<UserModel>().HasOne(j => j.JobCategory);

            modelBuilder.Entity<Weather>().HasData(new Weather
            {               
                WeightValue = 6.70,
                AirPressure = 1,
                Temperature = 60
                
            
            });
        }
    }
}
