using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using PreFlightAI.Server.Services;
using PreFlightAI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services
{
    public class GradientDescent
    {

        private readonly WeatherDataService weatherDataService;
        public async Task fetchWeather(int userId)
        {
            await weatherDataService.GetWeatherDetails(userId);
        }

        public async Task predictDensity(Weather weather, int userId)
        {
            if (weather == null)
            {
                await fetchWeather(userId);
            }

            List<double> list = new List<double> { fetchWeather };
            int number = 9;

            double closest = list.Aggregate((x, y) => Math.Abs(x - number) < Math.Abs(y - number) ? x : y);


            
          
            }
        }
    }
}
