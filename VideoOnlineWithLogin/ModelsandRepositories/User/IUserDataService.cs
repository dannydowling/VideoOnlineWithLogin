using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnlineWithLogin.Shared;

namespace VideoOnlineWithLogin.Server.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<typedUser>> GetAllUsers();
        Task<typedUser> GetUserDetails(int userId);
        Task<typedUser> AddUser(typedUser user);
        Task UpdateUser(typedUser user);
        Task DeleteUser(int userId);
    }
}
