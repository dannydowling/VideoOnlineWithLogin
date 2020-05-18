using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.Mapping
{
    public class DistanceConversion
    {
        public DistanceConversion Kilometers = new DistanceConversion(1.609344);
        public DistanceConversion NauticalMiles = new DistanceConversion(0.8684);
        public DistanceConversion Miles = new DistanceConversion(1);


        private readonly double _fromMilesFactor;

        private DistanceConversion(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input, DistanceConversion unit)
        {
            return input * _fromMilesFactor;
        }
    }
}
