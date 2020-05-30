using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;

namespace PreFlightAI.Server.Services
{
    public interface ILocationDataService
    {
        Task<IEnumerable<Location>> GetAllLocations();

        Task<IEnumerable<Location>> GetLocationsByEmployeeId(int employeeId);
        Task<Location> GetLocationById(int locationId);
    }
}
