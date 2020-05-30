using PreFlightAI.Server.Services;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.Services.Mapping
{
    public class DistanceTo
    {
        private readonly LocationDataService locationDataService;
        private readonly DistanceConversion convertDistance;
        private readonly SpeedConversion speedConversion;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public Dictionary<double, double> velocity;


        public DistanceTo()
        {
            // TODO:
            // The slope of e (exp) -x (exp) 2 ... e^-x^2
            // e to the negative x squared gives a bell curve
            // maybe we could use that to estimate fuel use to within a range.
            // https://www.youtube.com/watch?v=4PDoT7jtxmw&feature=youtu.be&t=1422

        }

        public async Task<dynamic> TravelTo(Coordinates baseLocation, Coordinates targetLocation, DistanceConversion unitOfLength, SpeedConversion speedUnit)
        {
            if (baseLocation == null)
            {
                LookupLocation location = new LookupLocation();
                await location.fetchLocation(_cancellationTokenSource.Token);
                //await locationDataService.GetLocationByCoordinates(baseLocation);
            }

            if (unitOfLength == null)
            {
                unitOfLength = convertDistance.NauticalMiles;
            }

            if (speedUnit == null)
            {
                speedUnit = speedConversion.MPH;
            }

            var bLAT = baseLocation.Latitude * (Math.PI / 180.0);
            var bLON = baseLocation.Longitude * (Math.PI / 180.0);
            var tLAT = targetLocation.Latitude * (Math.PI / 180.0);
            var tLON = targetLocation.Longitude * (Math.PI / 180.0) - bLON;

            var RadianConvertedValue
                                        = Math.Pow(
                Math.Sin((tLAT - bLAT) / 2.0), 2.0)             //A circle is a double Sine function.
                + Math.Cos(bLAT) * Math.Cos(tLAT)
                * Math.Pow(Math.Sin(tLON / 2.0), 2.0);

            var dist
                = 6376500.0 * (2.0 * Math.Atan2(
                Math.Sqrt(RadianConvertedValue), 
                Math.Sqrt(1.0 - RadianConvertedValue)));

            dist = convertDistance.ConvertFromMiles(dist, unitOfLength);
            var speed = speedConversion.ConvertFromMPH(dist, speedUnit);

            velocity = new Dictionary<double, double>();

            velocity.Add(dist, speed);
            return velocity;

        }
    }
}

