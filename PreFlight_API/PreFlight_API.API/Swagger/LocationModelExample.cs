using PreFlight_API.API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;

namespace PreFlight_API.API.Swagger
{
    public class LocationModelExample : IExamplesProvider<Location>
    {
        public Employee GetExamples()
        {
            var dnow = DateTime.UtcNow;
            return new Location
            {
                Id = Guid.NewGuid(),
                Name = "Juneau Airport",
                Street = "Egan Drive",
                Zip = "99801",
                City =  "Juneau",
                Country = Country.America
               
            };
        }
    }
}
