using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;

namespace PreFlightAI.Server.Services
{
    public class LocationDataService : ILocationDataService
    {
        private readonly HttpClient _clientLocation;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            {
                _clientLocation = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
                _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            }
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Location>>
                (await _clientLocation.GetStreamAsync($"api/location"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Location> GetLocationById(int locationId)
        {
            return await JsonSerializer.DeserializeAsync<Location>
                (await _clientLocation.GetStreamAsync($"api/location{locationId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Location> GetLocationByCoordinates(Coordinates coordinates)
        {
            return await JsonSerializer.DeserializeAsync<Location>
                (await _clientLocation.GetStreamAsync($"api/location{coordinates}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
