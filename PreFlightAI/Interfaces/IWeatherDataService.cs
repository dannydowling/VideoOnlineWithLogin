using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;

namespace PreFlightAI.Server.Services
{
    public interface IWeatherDataService
    {
        Task<IEnumerable<Weather>> GetAllWeatherForDensity();
        Task<IEnumerable<Weather>> GetAllWeatherForLocation(int locationId);

        Task<IEnumerable<Weather>> GetAllWeatherForLocationInRange(int locationId, TimeSpan timeSpan);
        Task<Weather> GetWeatherDetails(int weatherId);
        Task<Weather> AddWeather(Weather weather);
        Task UpdateWeather(Weather weather);
        Task DeleteWeather(int weatherId);
    }
}
