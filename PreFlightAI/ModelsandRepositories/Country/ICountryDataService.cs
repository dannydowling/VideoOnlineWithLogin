using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public interface ICountryDataService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountryById(int countryId);
    }
}
