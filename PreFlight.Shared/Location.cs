using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PreFlightAI.Shared
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }


    }

    public class Coordinates
    {
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
