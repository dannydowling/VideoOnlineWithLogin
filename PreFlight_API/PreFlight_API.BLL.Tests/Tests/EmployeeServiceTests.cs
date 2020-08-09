using AutoFixture;
using PreFlight_API.BLL.Tests.Helpers;
using Xunit;
using PreFlight_API.DAL.MySql.Models;
using System;
using Newtonsoft.Json;
using PreFlight_API.BLL.Models;

namespace PreFlight_API.BLL.Tests
{
    public class EmployeeServiceTests
    {
        [Theory]
        [InlineData("Danny Dowling", JobType.Owner)]
      
        public void CreateEmployee_Test(string Name, JobCategory jobType)
        {
            //Arrange
            var fixture = new Fixture();

            var employee = Fixtures.EmployeeFixture(Name, jobType);
            var mapper = Mapper.GetAutoMapper();
            var employeeRepoMoq = EmployeeMoqs.EmployeeRepositoryMoq(mapper.Map<EmployeeEntity>(employee));
            var employeeSvc = new EmployeeService(mapper, employeeRepoMoq.Object);

            //Act
            var newEmployee = employeeSvc.CreateEmployeeAsync(employee).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(employee);
            var expected = JsonConvert.SerializeObject(newEmployee);
            Assert.Equal(expected.Trim(), actual.Trim());
        }

        [Fact]
        public void GetEmployee_Test()
        {
            //Arrange
            var fixture = new Fixture();

            var employee = Fixtures.EmployeeFixture();
            var mapper = Mapper.GetAutoMapper();
            var employeeRepoMoq = EmployeeMoqs.EmployeeRepositoryMoq(mapper.Map<EmployeeEntity>(employee));
            var employeeSvc = new EmployeeService(mapper, employeeRepoMoq.Object);

            //Act
            var result = employeeSvc.GetEmployeeAsync(fixture.Create<Guid>()).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(employee);
            var expected = JsonConvert.SerializeObject(result);
            Assert.Equal(expected.Trim(), actual.Trim());
        }
    }
}
