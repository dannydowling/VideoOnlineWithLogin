﻿using System.Collections.Generic;
using System.Linq;
using PreFlightAI.Shared;
using PreFlight.AI.Server.Services.SQL;
using PreFlightAI.Shared.Employee;

namespace PreFlightAI.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ServerDbContext _appDbContext;

        public EmployeeRepository(ServerDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _appDbContext.Employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _appDbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);
        }

        public IEnumerable<Employee> GetEmployeesByLocation(int locationId)
        {
            return _appDbContext.Employees.Where(c => c.LocationId == locationId).ToList();
        }

        public Employee AddEmployee(Employee employee)
        {
            var addedEntity = _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var foundEmployee = _appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (foundEmployee != null)
            {
                foundEmployee.LocationId = employee.LocationId;
                foundEmployee.BirthDate = employee.BirthDate;
                foundEmployee.City = employee.City;
                foundEmployee.Email = employee.Email;
                foundEmployee.FirstName = employee.FirstName;
                foundEmployee.LastName = employee.LastName;
                foundEmployee.PhoneNumber = employee.PhoneNumber;
                foundEmployee.Street = employee.Street;
                foundEmployee.Zip = employee.Zip;
                foundEmployee.JobCategoryId = employee.JobCategoryId;
                foundEmployee.Comment = employee.Comment;
                foundEmployee.ExitDate = employee.ExitDate;
                foundEmployee.JoinedDate = employee.JoinedDate;
                foundEmployee.Password = employee.Password;
                

        _appDbContext.SaveChanges();

                return foundEmployee;
            }

            return null;
        }

        public void DeleteEmployee(int employeeId)
        {
            var foundEmployee = _appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (foundEmployee == null) return;

            _appDbContext.Employees.Remove(foundEmployee);
            _appDbContext.SaveChanges();
        }
    }
}
