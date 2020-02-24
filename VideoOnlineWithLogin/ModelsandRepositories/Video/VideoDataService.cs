using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VideoOnlineWithLogin.Shared;

namespace VideoOnlineWithLogin.Server.Services
{
    public class VideoDataService : IVideoDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VideoDataService()
        {
        }

        // The httpContextAccessor is registered in configure services, then accessible in any class.
        // gives information on the context the user is running in. Such as authenticated...
        public VideoDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<IEnumerable<Video>> GetAllVideos()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Video>>
                (await _httpClient.GetStreamAsync($"api/user/video"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Video> GetVideoDetails(int userId)
        {
            return await JsonSerializer.DeserializeAsync<Video>
                (await _httpClient.GetStreamAsync($"api/user/{userId}/video"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Video> AddVideo(Video video)
        {
            var videoJson =
                new StringContent(JsonSerializer.Serialize(video), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/user/video", videoJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Video>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateVideo(Video video)
        {
            var videoJson =
                new StringContent(JsonSerializer.Serialize(video), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/user/video", videoJson);
        }

        public async Task DeleteVideo(int videoId)
        {
            await _httpClient.DeleteAsync($"api/user/{videoId}");
        }
    }
}
