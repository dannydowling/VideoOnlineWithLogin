using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services
{
    public class FuelDensity
    {
        public double CurrentFuelDensity(Weather weather)
        {

            var calculation = 820.462;                                          //thermal expansion coefficient
                                                                                // I looked up pure Jet-A as 840, ours is more around 820 with additives.
            calculation = calculation * weather.Temperature;
            double calccomposite = calculation + weather.AirPressure;
            calculation = calccomposite / 15000;                            //bulk modulus of oil offset
            var offset = 254;

            calculation = (((1.55 * calculation) + offset) / 38.5);

            Weather currentWeather = new Weather();
            return Math.Round(calculation, 2);
        }
    }
}
