using PreFlight.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreFlight.Interfaces
{
    public interface IWeather
    {
        IEnumerable<Weather> GetWeathers();
        Weather GetWeatherByID(int? id);
        void InsertWeather(Weather weather);
        void DeleteWeather(int weatherId);
        void UpdateWeather(Weather weather);
        void Save();
    }
}