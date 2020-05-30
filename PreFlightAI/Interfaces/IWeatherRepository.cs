using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;

namespace PreFlightAI.Api.Models
{
    public interface IWeatherRepository
    {
        IEnumerable<Weather> GetForecast();
        Weather GetWeatherById(int weatherId);
        Weather AddWeather(Weather weather);
        Weather UpdateWeather(Weather weather);
        void DeleteWeather(int weatherId);
    }
}
