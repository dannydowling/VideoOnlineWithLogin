using System.Collections.Generic;
using System.Linq;
using PreFlight.AI.Server.Services.SQL;
using PreFlightAI.Shared;

namespace PreFlightAI.Api.Models
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _appDbContext;

        public LocationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return _appDbContext.Locations;
        }

        public Location GetLocationById(int locationId)
        {
            return _appDbContext.Locations.FirstOrDefault(c => c.LocationId == locationId);
        }
    }
}
