using System.Collections.Generic;
using System.Linq;
using PreFlightAI.Shared;
using PreFlightAI.Data;

namespace PreFlightAI.Api.Models
{
    public class WeatherRepository : IWeatherRepository
    {
        private AppDbContext _appDbContext;

        public WeatherRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Weather> GetForecast()
        {
            return _appDbContext.Weathers;
        }

        public List<Weather> GetWeatherById(double AirPressure, double Temperature)
        {
            
                var weathersAtTemperature = _appDbContext.Weathers.Where(e => e.Temperature == Temperature);
                var weathersAtAirPressure = _appDbContext.Weathers.Where(e => e.AirPressure == AirPressure);

                var candidateWeathers = weathersAtTemperature.Union(weathersAtAirPressure);
                return candidateWeathers.ToList();
        }

        public Weather AddWeather(Weather weather)
        {
            var addedEntity = _appDbContext.Weathers.Add(weather);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public List<Weather> UpdateWeather(Weather weather)
        {
            var weathersAtTemperature = _appDbContext.Weathers.Where(e => e.Temperature == weather.Temperature);
            var weathersAtAirPressure = _appDbContext.Weathers.Where(e => e.AirPressure == weather.AirPressure);

            var candidateWeathers = weathersAtTemperature.Union(weathersAtAirPressure);


            if (candidateWeathers != null)
            {
                foreach (var item in candidateWeathers)
                {
                    item.AirPressure = weather.AirPressure;
                    item.Temperature = weather.Temperature;                    
                }              

                _appDbContext.SaveChanges();

                return candidateWeathers.ToList();
            }

            return null;
        }

        public void DeleteWeather(double AirPressure, double Temperature)
        {
            var weathersAtTemperature = _appDbContext.Weathers.Where(e => e.Temperature == Temperature);
            var weathersAtAirPressure = _appDbContext.Weathers.Where(e => e.AirPressure == AirPressure);

            var candidateWeathers = weathersAtTemperature.Union(weathersAtAirPressure);

            if (candidateWeathers == null) return;

            foreach (var item in candidateWeathers)
            {
                _appDbContext.Weathers.Remove(item);
            }            
            _appDbContext.SaveChanges();
        }
    }
}
