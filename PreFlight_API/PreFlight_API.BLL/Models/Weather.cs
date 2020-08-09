using System;
using System.ComponentModel.DataAnnotations;

namespace PreFlight_API.BLL.Models
{
    public class Weather
    {

        [Key]
        public Guid Id { get; set; }
        public double AirPressure { get; set; }
        public double Temperature { get; set; }
        public double WeightValue { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}    
