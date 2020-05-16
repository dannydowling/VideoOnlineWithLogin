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
        public DbSet<Country> Countries { get; set; }
        public DbSet<typedUser> typedUsers { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 1, Name = "Belgium" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 2, Name = "Germany" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 3, Name = "Netherlands" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 4, Name = "USA" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 5, Name = "Japan" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 6, Name = "China" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 7, Name = "UK" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 8, Name = "France" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 9, Name = "Brazil" });

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
                CountryId = 4,
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
