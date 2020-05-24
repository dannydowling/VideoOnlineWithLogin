using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlightAI.Shared
{
    public class Weather
    {

        [Key]
        public int weatherID { get; set; }
        public double AirPressure { get; set; }
        public double Temperature { get; set; }
        public double WeightValue { get; set; }
        public Uri weatherLink { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}    
