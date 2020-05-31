using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared.Users;
using Microsoft.AspNetCore.Http;

namespace PreFlightAI.Server.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _clientUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
                

        // The httpContextAccessor is registered in configure services, then accessible in any class.
        // gives information on the context the user is running in. Such as authenticated...
        public UserDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            
            _clientUser = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));                
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }
        
        public async Task<IEnumerable<typedUser>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<typedUser>>
                (await _clientUser.GetStreamAsync($"api/user"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<typedUser> GetUserDetails(int userId)
        {
            return await JsonSerializer.DeserializeAsync<typedUser>
                (await _clientUser.GetStreamAsync($"api/user/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<typedUser> AddUser(typedUser user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _clientUser.PostAsync("api/user", userJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<typedUser>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateUser(typedUser user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            await _clientUser.PutAsync("api/user", userJson);
        }

        public async Task DeleteUser(int userId)
        {
            await _clientUser.DeleteAsync($"api/user/{userId}");
        }
    }
}
