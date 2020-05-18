using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class LocationDataService : ILocationDataService
    {
        private readonly HttpClient _httpClient;

        public LocationDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Location>>
                (await _httpClient.GetStreamAsync($"api/location"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Location> GetLocationById(int locationId)
        {
            return await JsonSerializer.DeserializeAsync<Location>
                (await _httpClient.GetStreamAsync($"api/location{locationId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Location> GetLocationByCoordinates(Coordinates coordinates)
        {
            return await JsonSerializer.DeserializeAsync<Location>
                (await _httpClient.GetStreamAsync($"api/location{coordinates}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
