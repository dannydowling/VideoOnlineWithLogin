using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreFlight.Types
{
    public class Weather
    {
        
        [Key]
        public int?  WeatherID { get; set; }
        public double AirPressure { get; set; }
        public double Temperature { get; set; }    
        public double Offset { get; set; }
        public double WeightValue { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Weather(int? Key, int Weight, double Airpressure, double Temperature)
            {           
                _weather.WeatherID = Key;
                _weather.AirPressure = Airpressure;
                _weather.Temperature = Temperature;
                _weather.WeightValue = Weight;
            }            
            Weather _weather;
        }
    }    
