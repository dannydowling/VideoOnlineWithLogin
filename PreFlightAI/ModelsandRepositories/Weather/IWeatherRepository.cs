using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Api.Models
{
    public interface IWeatherRepository
    {
        IEnumerable<Weather> GetForecast();
        List<Weather> GetWeatherById(double AirPressure, double Temperature);
        Weather AddWeather(Weather weather);
        List<Weather> UpdateWeather(Weather weather);
        void DeleteWeather(double AirPressure, double Temperature);
    }
}
