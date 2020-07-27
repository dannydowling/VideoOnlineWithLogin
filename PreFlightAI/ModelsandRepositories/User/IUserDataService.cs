using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserDetails(int userId);
        Task<UserModel> AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(int userId);
    }
}
