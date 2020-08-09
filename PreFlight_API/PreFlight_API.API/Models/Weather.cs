using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlight_API.API.Models
{
    public class Weather
    {
       
        public Guid Id { get; set; }
        public double AirPressure { get; set; }
        public double Temperature { get; set; }
        public double WeightValue { get; set; }
        public Uri weatherLink { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}    
