using PreFlight_API.API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PreFlight_API.API.Swagger
{
    public class UserModelExample : IExamplesProvider<UserModel>
    {
        public UserModel GetExamples()
        {
            var dnow = DateTime.UtcNow;
            return new UserModel
            {
            Id = Guid.NewGuid(),
            FirstName = "Danny",
            LastName = "Dowling",
            Email = "KauraiRentals@gmail.com",
            CodeComment ="",
            RowVersion = dnow,
            JoinedDate = dnow,
            Weathers = { },
            Password = "Password"
               
            };
        }
    }
}
