using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Api.Models
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAllLocations();
        Location GetLocationById(int locationId);
    }
}
