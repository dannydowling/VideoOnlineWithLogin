using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public interface IWeatherDataService
    {
        Task<IEnumerable<Weather>> GetForecast();
        Task<List<Weather>> GetWeatherDetails(int userId);
        Task<Weather> AddWeather(Weather weather);
        Task UpdateWeather(Weather weather);
        Task DeleteWeather(double AirPressure, double Temperature);
    }
}
