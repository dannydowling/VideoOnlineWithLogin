using PreFlightAI.Shared.Places;
using System;
using System.Collections.Generic;


namespace PreFlightAI.Api.Models
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAllLocations();
        Location GetLocationById(int locationId);
    }
}
