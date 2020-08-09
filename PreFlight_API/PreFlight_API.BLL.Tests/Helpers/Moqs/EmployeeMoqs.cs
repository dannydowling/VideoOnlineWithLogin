using AutoFixture;
using Moq;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Tests.Helpers
{
    public static class EmployeeMoqs
    {
        public static Mock<IEmployeeService> EmployeeServiceMoq()
        {
            var fixture = new Fixture();

            var moq = new Mock<IEmployeeService>(MockBehavior.Strict);
            moq.Setup(s => s.CreateEmployeeAsync(It.IsAny<Employee>()))
              .ReturnsAsync(fixture.Build<Employee>().Create());
            moq.Setup(s => s.GetEmployeeAsync(It.IsAny<Guid>()))
             .ReturnsAsync(fixture.Build<Employee>().Create());
            moq.Setup(s => s.UpdateEmployeeAsync(It.IsAny<Employee>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteEmployeeAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetEmployeeListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<Employee>().CreateMany(20));

            return moq;
        }

        public static Mock<IEmployeeRepository> EmployeeRepositoryMoq(EmployeeEntity employeeEntity)
        {
            var fixture = new Fixture();

            var moq = new Mock<IEmployeeRepository>(MockBehavior.Strict);
            moq.Setup(s => s.CreateCarAsync(It.IsAny<EmployeeEntity>()))
              .ReturnsAsync(employeeEntity);
            moq.Setup(s => s.GetEmployeeAsync(It.IsAny<Guid>()))
             .ReturnsAsync(employeeEntity);
            moq.Setup(s => s.UpdateEmployeeAsync(It.IsAny<EmployeeEntity>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteEmployeeAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetEmployeeListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<EmployeeEntity>().CreateMany(20));

            return moq;
        }
    }
}
