using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using PreFlight_API.DAL.MySql.Contract;
using System;
using Renci.SshNet;
using PreFlight_API.DAL.MySql.Models;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;

namespace PreFlightAI.Server.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IWeatherRepository _weatherRepo { get; }

        public IEnumerable<WeatherEntity> weathers { get; set; }

        public WeatherService()
        {
        }

        
        public WeatherService(IMapper mapper, IWeatherRepository weatherRepo, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weatherRepo = weatherRepo ?? throw new ArgumentNullException(nameof(weatherRepo));
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IEnumerable<Weather>> GetWeatherListAsync(int pageNumber, int pageSize, double? AirPressure, double? Temperature)
        {           
            try
            {
                weathers = await _weatherRepo.GetWeatherListAsync(pageNumber, pageSize, AirPressure, Temperature);
            }
            catch (NullReferenceException)
            {
               weathers = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherEntity>>
                (await _httpClient.GetStreamAsync($"/api/weather"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return _mapper.Map<IEnumerable<Weather>>(weathers);
        }

  
        public async Task<IEnumerable<Weather>> GetWeatherByAirPressureAsync(double AirPressure)
        {
            weathers = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherEntity>>
                (await _httpClient.GetStreamAsync($"/api/weather/{AirPressure}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return _mapper.Map<IEnumerable<Weather>>(weathers);
        }

        public async Task<IEnumerable<Weather>> GetWeatherByTemperatureAsync(double Temperature)
        {
            weathers = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherEntity>>
                (await _httpClient.GetStreamAsync($"/api/weather/{Temperature}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return _mapper.Map<IEnumerable<Weather>>(weathers);
        }

        public async Task<Weather> AddWeather(Weather weather)
        {
            var weatherJson =
                new StringContent(JsonSerializer.Serialize(weather), Encoding.UTF8, "application/json");

            var responseAirTemp = await _httpClient.PostAsync($"/api/weather/{weather.Temperature}", weatherJson);

            if (responseAirTemp.IsSuccessStatusCode)
            {
                await _httpClient.PostAsync($"/api/weather/{weather.AirPressure}", weatherJson);
                return await JsonSerializer.DeserializeAsync<Weather>(await responseAirTemp.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateWeather(Weather weather)
        {
            var weatherJson =
                new StringContent(JsonSerializer.Serialize(weather), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"/api/weather/{weather.Id}", weatherJson);
        }

        public async Task DeleteWeather(double AirPressure, double Temperature)
        {
            await _httpClient.DeleteAsync($"/api/user/weather/{AirPressure}, {Temperature}");
        }
    }
}
