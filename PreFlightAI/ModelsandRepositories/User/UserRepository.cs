using System.Collections.Generic;
using System.Linq;
using PreFlightAI.Shared;
using PreFlightAI.Data;

namespace PreFlightAI.Api.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _appDbContext.typedUsers;
        }

        public UserModel GetUserById(int userId)
        {
            return _appDbContext.typedUsers.FirstOrDefault(c => c.UserID == userId);
        }

        public UserModel AddUser(UserModel user)
        {
            var addedEntity = _appDbContext.typedUsers.Add(user);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public UserModel UpdateUser(UserModel user)
        {
            var foundUser = _appDbContext.typedUsers.FirstOrDefault(e => e.UserID == user.UserID);

            if (foundUser != null)
            {              
                foundUser.Email =           user.Email;
                foundUser.FirstName =       user.FirstName;
                foundUser.LastName =        user.LastName;               
                foundUser.Comment =         user.Comment;
                foundUser.ExitDate =        user.ExitDate;
                foundUser.JoinedDate =      user.JoinedDate;
                foundUser.Password =        user.Password;

                _appDbContext.SaveChanges();

                return foundUser;
            }

            return null;
        }

        public void DeleteUser(int userId)
        {
            var foundUser = _appDbContext.typedUsers.FirstOrDefault(e => e.UserID == userId);
            if (foundUser == null) return;

            _appDbContext.typedUsers.Remove(foundUser);
            _appDbContext.SaveChanges();
        }
    }
}
