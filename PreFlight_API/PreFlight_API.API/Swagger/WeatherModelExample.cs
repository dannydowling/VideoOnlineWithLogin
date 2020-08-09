using PreFlight_API.API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace PreFlight_API.API.Swagger
{
    public class WeatherModelExample : IExamplesProvider<Weather>
    {
        public Weather GetExamples()
        {
            var dnow = DateTime.UtcNow;
            return new Weather
            {
               Id = Guid.NewGuid(),
               AirPressure = 1013,
               Temperature = 60,
               WeightValue = 6.70,
               RowVersion = dnow
               
            };
        }
    }
}
