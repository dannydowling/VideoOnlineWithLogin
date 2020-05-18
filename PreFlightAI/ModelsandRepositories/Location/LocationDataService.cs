using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PreFlight.AI.Server.Services.HttpClients;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class LocationDataService : ILocationDataService
    {
        private readonly positioningHttpClient clientLocation;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LocationDataService(positioningHttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            clientLocation = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            clientLocation.BaseAddress = new Uri("http://localhost:46633/");
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Location>>
                (await clientLocation.GetStreamAsync($"api/location"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Location> GetLocationById(int locationId)
        {
            return await JsonSerializer.DeserializeAsync<Location>
                (await clientLocation.GetStreamAsync($"api/location{locationId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Location> GetLocationByCoordinates(Coordinates coordinates)
        {
            return await JsonSerializer.DeserializeAsync<Location>
                (await clientLocation.GetStreamAsync($"api/location{coordinates}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
