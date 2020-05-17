using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Location> Locations { get; set; }
        public DbSet<typedUser> typedUsers { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }


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

            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 1, JobCategoryName = "Guest" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 2, JobCategoryName = "Visitor" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 3, JobCategoryName = "Verified" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 4, JobCategoryName = "Worker" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 5, JobCategoryName = "IT Worker" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 6, JobCategoryName = "IT Lead" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 7, JobCategoryName = "Manager" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 8, JobCategoryName = "Senior Manager" });
            modelBuilder.Entity<JobCategory>().HasData(new JobCategory() { JobCategoryId = 9, JobCategoryName = "Owner" });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                LocationId = 4,
                JobCategoryId = 9,
                Password = "Password",                                
                BirthDate = new DateTime(1979, 1, 16),
                City = "Juneau",
                Email = "danny.dowling@gmail.com",
                FirstName = "Danny",
                LastName = "Dowling",
                PhoneNumber = "324777888773",
                Street = "1 Grimoire Place",
                Zip = "99801",
                Comment = "Using Fake Address and Phone number here",
                ExitDate = null,
                JoinedDate = new DateTime(2019, 3, 1)
            });
            

            modelBuilder.Entity<typedUser>().HasData(new typedUser
            {
                userId = 1,
                Email = "danny.dowling@gmail.com",
                FirstName = "Danny",
                LastName = "Dowling",
                Password = "password",
                Comment = "Using Fake Address and Phone number here",
                ExitDate = null,
                JoinedDate = new DateTime(2019, 3, 1)
            });

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
