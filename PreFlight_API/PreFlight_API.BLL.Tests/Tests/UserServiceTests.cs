using AutoFixture;
using PreFlight_API.BLL.Tests.Helpers;
using Xunit;
using PreFlight_API.DAL.MySql.Models;
using System;
using Newtonsoft.Json;
using PreFlight_API.BLL.Models;

namespace PreFlight_API.BLL.Tests
{
    public class UserServiceTests
    {
        [Theory]      
        public void CreateUser_Test(string firstName, string lastName, string email)
        {
            //Arrange
            var fixture = new Fixture();

            var user = Fixtures.UserFixture(firstName, lastName, email);
            var mapper = Mapper.GetAutoMapper();
            var userRepoMoq = UserMoqs.UserRepositoryMoq(mapper.Map<UserEntity>(user));
            var userSvc = new UserService(mapper, userRepoMoq.Object);

            //Act
            var newUser = userSvc.CreateUserAsync(user).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(user);
            var expected = JsonConvert.SerializeObject(newUser);
            Assert.Equal(expected.Trim(), actual.Trim());
        }

        [Fact]
        public void GetUser_Test()
        {
            //Arrange
            var fixture = new Fixture();

            var user = Fixtures.UserFixture();
            var mapper = Mapper.GetAutoMapper();
            var userRepoMoq = UserMoqs.UserRepositoryMoq(mapper.Map<UserEntity>(user));
            var userSvc = new UserService(mapper, userRepoMoq.Object);

            //Act
            var result = userSvc.GetUserAsync(fixture.Create<Guid>()).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(user);
            var expected = JsonConvert.SerializeObject(result);
            Assert.Equal(expected.Trim(), actual.Trim());
        }
    }
}
