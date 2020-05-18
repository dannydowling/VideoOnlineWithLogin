using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using Microsoft.AspNetCore.Http;
using PreFlight.AI.Server.Services.HttpClients;
using System;

namespace PreFlightAI.Server.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly userHttpClient clientUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
                

        // The httpContextAccessor is registered in configure services, then accessible in any class.
        // gives information on the context the user is running in. Such as authenticated...
        public UserDataService(userHttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            clientUser = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            clientUser.BaseAddress = new Uri("http://localhost:46633/");
        }
        
        public async Task<IEnumerable<typedUser>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<typedUser>>
                (await clientUser.GetStreamAsync($"api/user"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<typedUser> GetUserDetails(int userId)
        {
            return await JsonSerializer.DeserializeAsync<typedUser>
                (await clientUser.GetStreamAsync($"api/user/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<typedUser> AddUser(typedUser user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await clientUser.PostAsync("api/user", userJson);

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

            await clientUser.PutAsync("api/user", userJson);
        }

        public async Task DeleteUser(int userId)
        {
            await clientUser.DeleteAsync($"api/user/{userId}");
        }
    }
}
