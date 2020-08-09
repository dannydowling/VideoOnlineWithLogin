using PreFlight_API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.BLL.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetLocationListAsync(int pageNumber, int pageSize);
        Task<Location> GetLocationAsync(Guid id);
        Task<Location> CreateLocationAsync(Location location);
        Task UpdateLocationAsync(Location location);
        Task DeleteLocationAsync(Location location);
    }
}
