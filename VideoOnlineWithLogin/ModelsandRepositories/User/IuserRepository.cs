using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Api.Models
{
    public interface IUserRepository
    {
        IEnumerable<typedUser> GetAllUsers();
        typedUser GetUserById(int userId);
        typedUser AddUser(typedUser user);
        typedUser UpdateUser(typedUser user);
        void DeleteUser(int userId);
    }
}
