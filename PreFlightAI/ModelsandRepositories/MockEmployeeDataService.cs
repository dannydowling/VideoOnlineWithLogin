using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlight.AI.Server.Data;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class MockEmployeeDataService : IEmployeeDataService
    {
        private List<Employee> _employees;
        private List<Country> _countries;
        private List<JobCategory> _jobCategories;
        private List<UserModel> _users;

        private IEnumerable<Employee> Employees
        {
            get
            {
                if (_employees == null)
                    InitializeEmployees();
                return _employees;
            }
        }

        private IEnumerable<UserModel> typedUsers
        {
            get
            {
                if (_users == null)
                    InitializeUsers();
                return _users;
            }
        }
               

        private List<Country> Countries
        {
            get
            {
                if (_countries == null)
                    InitializeCountries();
                return _countries;
            }
        }

        private List<JobCategory> JobCategories
        {
            get
            {
                if (_jobCategories == null)
                    InitializeJobCategories();
                return _jobCategories;
            }
        }

        private void InitializeJobCategories()
        {
            _jobCategories = new List<JobCategory>()
            {
                new JobCategory{JobCategoryId = 1, JobCategoryName = "Guest" },
                new JobCategory{JobCategoryId = 2, JobCategoryName = "Visitor" },
                new JobCategory{JobCategoryId = 3, JobCategoryName = "Verified" },
                new JobCategory{JobCategoryId = 4, JobCategoryName = "Worker" },
                new JobCategory{JobCategoryId = 5, JobCategoryName = "IT Worker" },
                new JobCategory{JobCategoryId = 6, JobCategoryName = "IT Lead" },
                new JobCategory{JobCategoryId = 7, JobCategoryName = "Manager" },
                new JobCategory{JobCategoryId = 8, JobCategoryName = "Senior Manager" }, 
                new JobCategory{JobCategoryId = 9, JobCategoryName = "Owner" }
            };
        }

        private void InitializeCountries()
        {
            _countries = new List<Country>();            
        }

        private void InitializeEmployees()
        {
            if (_employees == null)
            {
                Employee e1 = new Employee
                {
                    EmployeeId = 1,
                    
                    Password = "password",
                    JobCategoryId = 9,
                    BirthDate = new DateTime(1979, 1, 16),
                    
                    Email = "danny.dowling@gmail.com",
                    FirstName = "Danny",
                    LastName = "Dowling",
                    PhoneNumber = "324777888773",
                   
                    ExitDate = null,
                    JoinedDate = new DateTime(2019, 3, 1)
                };
                _employees = new List<Employee>() { e1 };
            }
        }

        private void InitializeUsers()
        {
            if (_users == null)
            {
                UserModel u1 = new UserModel
                {
                    UserID = 1,                    
                    Email = "danny.dowling@gmail.com",
                    FirstName = "Danny",
                    LastName = "Dowling",
                    Comment = "Using Fake Address and Phone number here",
                    ExitDate = null,
                    JoinedDate = new DateTime(2019, 3, 1)

                };
                _users = new List<UserModel>() { u1 };
            } 
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await Task.Run(() => Employees);
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await Task.Run(() => Countries);
        }

        public async Task<List<JobCategory>> GetAllJobCategories()
        {
            return await Task.Run(() => JobCategories);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await Task.Run(() => typedUsers);
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            return await Task.Run(() => { return typedUsers.FirstOrDefault(e => e.UserID == userId); });
        }
        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await Task.Run(() => { return Employees.FirstOrDefault(e => e.EmployeeId == employeeId); });
        }

        public Task<Employee> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
