using Newtonsoft.Json.Linq;
using PreFlightAI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace PreFlight.AI.Server.Services
{
    public class WeatherLookup
    {
        private readonly FuelDensity fuelDensity;
        private double WebreturnedAirpressure;
        private double WebreturnedTemperature;
        public Weather CurrentWeather(string icao, HttpClient client)
        {
            Weather weather = new Weather();

            var url = string.Format("https://api.weather.gov/stations/{0}/observations/current", icao);
            var uri = new Uri(url, UriKind.Absolute);
            var response = client.GetStringAsync(uri).Result;

            JObject w = JObject.Parse(response);

            WebreturnedTemperature = Convert.ToInt32(w.SelectToken("properties.temperature.value"));
            WebreturnedAirpressure = Convert.ToInt32(w.SelectToken("properties.barometricPressure.value"));

            //we tested pajn with a hydrometer to get 6.73 at 60 degrees in 1014 mb pressure.

            WebreturnedTemperature = WebreturnedTemperature * 9;                //conversion to Farenheit
            WebreturnedTemperature = WebreturnedTemperature / 5;
            weather.Temperature = WebreturnedTemperature + 32;
            weather.AirPressure = WebreturnedAirpressure / 100;                   // into pascals
            
            try
            {
                weather.WeightValue = fuelDensity.CurrentFuelDensity(weather);
            }
            catch (Exception)
            {
                weather.WeightValue = 6.70;
            }

            return weather;
        }          
    }
}