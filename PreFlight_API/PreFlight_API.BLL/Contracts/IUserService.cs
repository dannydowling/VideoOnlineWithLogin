using PreFlight_API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.BLL.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUserListAsync(int pageNumber, int pageSize);
        Task<UserModel> GetUserById(Guid id);
        Task<UserModel> AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(Guid id);
    }
}
