using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using Microsoft.AspNetCore.Http;

namespace PreFlightAI.Server.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDataService()
        {
        }

        // The httpContextAccessor is registered in configure services, then accessible in any class.
        // gives information on the context the user is running in. Such as authenticated...
        public UserDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }
        
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UserModel>>
                (await _httpClient.GetStreamAsync($"api/user"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserModel> GetUserDetails(int userId)
        {
            return await JsonSerializer.DeserializeAsync<UserModel>
                (await _httpClient.GetStreamAsync($"api/user/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/user", userJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<UserModel>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateUser(UserModel user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/user", userJson);
        }

        public async Task DeleteUser(int userId)
        {
            await _httpClient.DeleteAsync($"api/user/{userId}");
        }
    }
}
