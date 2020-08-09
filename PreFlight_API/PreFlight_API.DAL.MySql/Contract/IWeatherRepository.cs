using PreFlight_API.DAL.MySql.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.DAL.MySql.Contract
{
    public interface IWeatherRepository
    {
       
        Task<WeatherEntity> CreateWeatherAsync(WeatherEntity weather);
        Task<WeatherEntity> GetWeatherAsync(Guid id);
        Task<bool> UpdateWeatherAsync(WeatherEntity weather);
        Task<bool> DeleteWeatherAsync(Guid id, double AirPressure, double Temperature);


         Task<IEnumerable<WeatherEntity>> GetWeatherListAsync(int pageNumber, int pageSize, double? AirPressure, double? Temperature);
    }
}
