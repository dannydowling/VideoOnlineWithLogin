using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Places;

namespace PreFlightAI.Server.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly HttpClient _clientWeather;
        private readonly IHttpContextAccessor _httpContextAccessor;
      

        public WeatherDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            {
                _clientWeather = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
                _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            }
        }

        public async Task<IEnumerable<Weather>> GetAllWeatherForDensity()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Weather>>
                (await _clientWeather.GetStreamAsync($"api/weather"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Weather>> GetAllWeatherForLocation(int locationId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Weather>>
                (await _clientWeather.GetStreamAsync($"api/weather/{locationId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Weather>> GetAllWeatherForLocationInRange(int locationId, TimeSpan timeSpan)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Weather>>
                (await _clientWeather.GetStreamAsync($"api/weather/{locationId}/{timeSpan}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Weather> GetWeatherDetails(int weatherId)
        {
            return await JsonSerializer.DeserializeAsync<Weather>
                (await _clientWeather.GetStreamAsync($"api/weather/{weatherId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Weather> AddWeather(Weather weather)
        {
            var weatherJson =
                new StringContent(JsonSerializer.Serialize(weather), Encoding.UTF8, "application/json");

            var response = await _clientWeather.PostAsync("api/weather", weatherJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Weather>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateWeather(Weather weather)
        {
            var weatherJson =
                new StringContent(JsonSerializer.Serialize(weather), Encoding.UTF8, "application/json");

            await _clientWeather.PutAsync("api/weather", weatherJson);
        }

        public async Task DeleteWeather(int weatherId)
        {
            await _clientWeather.DeleteAsync($"api/{weatherId}");
        }
    }
}
