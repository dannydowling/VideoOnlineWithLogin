using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;


namespace PreFlightAI.Server.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ILocationRepository _locationRepo { get; }

        public IEnumerable<LocationEntity> locations { get; private set; }

        public LocationService(IMapper mapper, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Location>> GetLocationListAsync(int pageNumber, int pageSize)
        {
            try
            {
                locations = await _locationRepo.GetLocationListAsync(pageNumber, pageSize);
            }
            catch (NullReferenceException)
            {

                await JsonSerializer.DeserializeAsync<IEnumerable<Location>>
                 (await _httpClient.GetStreamAsync($"api/location"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return _mapper.Map<IEnumerable<Location>>(locations);
        }

        public async Task<Location> GetLocationAsync(Guid id)
        {
            var location = await JsonSerializer.DeserializeAsync<Location>
                (await _httpClient.GetStreamAsync($"api/location{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return _mapper.Map<Location>(location);
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            var locationJson =
           new StringContent(JsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/location", locationJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Location>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }


        public async Task UpdateLocationAsync(Location location)
        {
            var locationJson =
                new StringContent(JsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/location", locationJson);
        }


        public async Task DeleteLocationAsync(Location location)
        {
            await _httpClient.DeleteAsync($"api/location/{location.Id}");
        }
    }
}
