using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PreFlightAI.Shared;

namespace PreFlight.AI.IDP.Data
{
    public class IDPContext : IdentityDbContext
    {
        public IDPContext(DbContextOptions<IDPContext> options)
            : base(options)
        {
        }

        public DbSet<typedUser> typedUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                LocationId = 4,
                JobCategoryId = 9,
                Password = "Password",
                BirthDate = new DateTime(1988, 1, 1),
                City = "Juneau",
                Email = "danny.dowling@gmail.com",
                FirstName = "Danny",
                LastName = "Dowling",
                PhoneNumber = "324777888773",
                Street = "1 Grimoire Place",
                Zip = "99801",
                Comment = "Using Fake Address and Phone number here",
                ExitDate = null,
                JoinedDate = new DateTime(2020, 5, 1)
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
        }
    }
}
