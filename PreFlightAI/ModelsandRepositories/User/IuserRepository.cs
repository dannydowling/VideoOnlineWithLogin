using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Api.Models
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserById(int userId);
        UserModel AddUser(UserModel user);
        UserModel UpdateUser(UserModel user);
        void DeleteUser(int userId);
    }
}
