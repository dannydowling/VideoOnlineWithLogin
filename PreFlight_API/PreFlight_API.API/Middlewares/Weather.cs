using Newtonsoft.Json.Linq;
using PreFlightAI.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace PreFlight_API.API.Middleware
{
    public class FetchFuelDensity
    {  
        private double WebreturnedTemperature;
        private double WebreturnedAirpressure;
        public double CurrentFuelDensity(string icao, HttpClient client)
        {           
      
            var url = string.Format("https://api.weather.gov/stations/{0}/observations/current", icao);
            var uri = new Uri(url, UriKind.Absolute);
            var response =client.GetStringAsync(uri).Result;

            JObject w = JObject.Parse(response);

            WebreturnedTemperature = Convert.ToInt32(w.SelectToken("properties.temperature.value"));
            WebreturnedAirpressure = Convert.ToInt32(w.SelectToken("properties.barometricPressure.value"));

            //we tested pajn with a hydrometer to get 6.73 at 60 degrees in 1014 mb pressure.

            WebreturnedTemperature = WebreturnedTemperature * 9;                //conversion to Farenheit
            WebreturnedTemperature = WebreturnedTemperature / 5;
            double withFarenheit = WebreturnedTemperature + 32;

            WebreturnedAirpressure = WebreturnedAirpressure / 100;                   // into pascals

            var calculation = 820.462;                                          //thermal expansion coefficient
                                                                                // I looked up pure Jet-A as 840, ours is more around 820 with additives.
            calculation = calculation * withFarenheit;
            double calccomposite = calculation + WebreturnedAirpressure;
            calculation = calccomposite / 15000;                            //bulk modulus of oil offset
            var offset = 254;

            calculation = (((1.55 * calculation) + offset) / 38.5);

            Weather currentWeather = new Weather();
            return Math.Round(calculation, 2);                       
        }
    }
}