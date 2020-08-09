using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlight_API.DAL.MySql.Models
{
    public class WeatherEntity
    {

        
        public string Id { get; set; }
        public double AirPressure { get; set; }
        public double Temperature { get; set; }
        public double WeightValue { get; set; }

        [DataType(DataType.Date)]
        public DateTime RowVersion { get; set; }

    }
}    
