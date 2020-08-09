using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using PreFlight_API.DAL.MySql.Models;
using System;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.BLL.Models;

namespace PreFlightAI.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IUserRepository _userRepo { get; }

        public IEnumerable<UserModelEntity> users { get; private set; }


        public UserService(IMapper mapper, IUserRepository userRepo, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _userRepo = userRepo ?? throw new System.ArgumentNullException(nameof(userRepo));
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public UserService()
        {
        }


        public async Task<IEnumerable<UserModel>> GetAllUserListAsync(int pageNumber, int pageSize)
        {
            try
            {
                users = await _userRepo.GetAllUserListAsync(pageNumber, pageSize);
            }
            catch (NullReferenceException)
            {
                users = await JsonSerializer.DeserializeAsync<IEnumerable<UserModelEntity>>
                                (await _httpClient.GetStreamAsync($"api/user"), 
                                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetUserById(Guid id)
        {
            var user =  await JsonSerializer.DeserializeAsync<UserModel>
                (await _httpClient.GetStreamAsync($"api/user/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return _mapper.Map<UserModel>(user);
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

        public async Task DeleteUser(Guid id)
        {
            await _httpClient.DeleteAsync($"api/user/{id}");
        }
    }
}
