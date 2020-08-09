using PreFlight_API.DAL.MySql.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.DAL.MySql.Contract
{
    public interface ILocationRepository
    {
        Task<LocationEntity> CreateLocationAsync(LocationEntity location);
        Task<LocationEntity> GetLocationAsync(Guid id);
        Task<bool> UpdateLocationAsync(LocationEntity location);
        Task<bool> DeleteLocationAsync(LocationEntity location);

        Task<IEnumerable<LocationEntity>> GetLocationListAsync(int pageNumber, int pageSize);

    }
}
