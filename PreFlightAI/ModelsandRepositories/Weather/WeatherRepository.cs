using System.Collections.Generic;
using System.Linq;
using PreFlightAI.Shared;
using PreFlight.AI.Server.Services.SQL;

namespace PreFlightAI.Api.Models
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AppDbContext _appDbContext;

        public WeatherRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Weather> GetForecast()
        {
            return _appDbContext.Weathers;
        }

        public Weather GetWeatherById(int weatherId)
        {
            return _appDbContext.Weathers.FirstOrDefault(c => c.weatherID == weatherId);
        }

        public Weather AddWeather(Weather weather)
        {
            var addedEntity = _appDbContext.Weathers.Add(weather);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Weather UpdateWeather(Weather weather)
        {
            var foundweather = _appDbContext.Weathers.FirstOrDefault(e => e.weatherID == weather.weatherID);

            if (foundweather != null)
            {
                foundweather.Temperature = weather.Temperature;
                foundweather.AirPressure = weather.AirPressure;
                foundweather.weatherLink = weather.weatherLink;

                _appDbContext.SaveChanges();

                return foundweather;
            }

            return null;
        }

        public void DeleteWeather(int weatherId)
        {
            var foundWeather = _appDbContext.Weathers.FirstOrDefault(e => e.weatherID == weatherId);
            if (foundWeather == null) return;

            _appDbContext.Weathers.Remove(foundWeather);
            _appDbContext.SaveChanges();
        }
    }
}
