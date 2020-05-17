using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public interface ILocationDataService
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> GetLocationById(int locationId);
    }
}
