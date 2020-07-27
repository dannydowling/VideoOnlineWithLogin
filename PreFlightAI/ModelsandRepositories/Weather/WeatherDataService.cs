using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherDataService()
        {
        }

        // The httpContextAccessor is registered in configure services, then accessible in any class.
        // gives information on the context the user is running in. Such as authenticated...
        public WeatherDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IEnumerable<Weather>> GetForecast()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Weather>>
                (await _httpClient.GetStreamAsync($"/api/weather/forecast"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Weather>> GetWeatherDetails(int userId)
        {
            return await JsonSerializer.DeserializeAsync<List<Weather>>
                (await _httpClient.GetStreamAsync($"/api/user/{userId}/weather"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Weather> AddWeather(Weather weather)
        {
            var weatherJson =
                new StringContent(JsonSerializer.Serialize(weather), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/weather", weatherJson);

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

            await _httpClient.PutAsync("/api/weather", weatherJson);
        }

        public async Task DeleteWeather(double AirPressure, double Temperature)
        {
            await _httpClient.DeleteAsync($"/api/user/weather/{AirPressure}, {Temperature}");
        }
    }
}
