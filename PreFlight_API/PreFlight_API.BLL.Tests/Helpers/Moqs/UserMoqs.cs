using AutoFixture;
using Moq;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Tests.Helpers
{
    public static class UserMoqs
    {
        public static Mock<IUserService> UserServiceMoq()
        {
            var fixture = new Fixture();

            var moq = new Mock<IUserService>(MockBehavior.Strict);
            moq.Setup(s => s.CreateUserAsync(It.IsAny<UserModel>()))
              .ReturnsAsync(fixture.Build<UserModel>().Create());
            moq.Setup(s => s.GetUserAsync(It.IsAny<Guid>()))
             .ReturnsAsync(fixture.Build<UserModel>().Create());
            moq.Setup(s => s.UpdateUserAsync(It.IsAny<UserModel>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetUserListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<UserModel>().CreateMany(20));

            return moq;
        }

        public static Mock<IUserRepository> UserRepositoryMoq(UserModelEntity userEntity)
        {
            var fixture = new Fixture();

            var moq = new Mock<IUserRepository>(MockBehavior.Strict);
            moq.Setup(s => s.CreateUserAsync(It.IsAny<UserModelEntity>()))
              .ReturnsAsync(userEntity);
            moq.Setup(s => s.GetUserAsync(It.IsAny<Guid>()))
             .ReturnsAsync(userEntity);
            moq.Setup(s => s.UpdateUserAsync(It.IsAny<UserModelEntity>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteUserAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetUserListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<UserModelEntity>().CreateMany(20));

            return moq;
        }
    }
}
