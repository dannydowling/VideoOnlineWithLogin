using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.Mapping
{
    public class SpeedConversion
    {      
            public SpeedConversion KPH = new SpeedConversion(1.609344);
            public SpeedConversion Knots = new SpeedConversion(0.8684);
            public SpeedConversion MPH = new SpeedConversion(1);


            private readonly double _fromMPHfactor;

            private SpeedConversion(double fromMPHfactor)
            {
                _fromMPHfactor = fromMPHfactor;
            }

            public double ConvertFromMPH(double input, SpeedConversion unit)
            {
                return input * _fromMPHfactor;
            }
        }
    }
}
