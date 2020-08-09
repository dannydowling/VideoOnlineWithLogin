using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlight_API.DAL.MySql.Models;

namespace PreFlight_API.DAL.MySql.Contract
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModelEntity>> GetAllUserListAsync(int pageNumber, int pageSize);
        Task<UserModelEntity> GetUserByIdAsync(Guid id);
        Task<UserModelEntity> AddUserAsync(UserModelEntity user);
        Task<bool> UpdateUserAsync(UserModelEntity user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
