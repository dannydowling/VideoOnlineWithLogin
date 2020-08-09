using PreFlight_API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.BLL.Contracts
{
    public interface IWeatherService
    {
        Task<IEnumerable<Weather>> GetWeatherListAsync(int pageNumber, int pageSize, double? AirPressure, double? Temperature);

        Task<IEnumerable<Weather>> GetWeatherByAirPressureAsync(double AirPressure);

        Task<IEnumerable<Weather>> GetWeatherByTemperatureAsync(double Temperature);
        Task<Weather> AddWeather(Weather weather);
        Task UpdateWeather(Weather weather);
        Task DeleteWeather(double AirPressure, double Temperature);
    }
}
