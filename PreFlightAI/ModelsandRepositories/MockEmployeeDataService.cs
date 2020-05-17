using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class MockEmployeeDataService : IEmployeeDataService
    {
        private List<Employee> _employees;
        private List<Location> _countries;
        private List<JobCategory> _jobCategories;
        private List<typedUser> _users;

        private IEnumerable<Employee> Employees
        {
            get
            {
                if (_employees == null)
                    InitializeEmployees();
                return _employees;
            }
        }

        private IEnumerable<typedUser> typedUsers
        {
            get
            {
                if (_users == null)
                    InitializeUsers();
                return _users;
            }
        }
               

        private List<Location> Countries
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
            _countries = new List<Location>
            {
                new Location {LocationId = 1, Name = "Belgium"},
                new Location {LocationId = 2, Name = "Netherlands"},
                new Location {LocationId = 3, Name = "USA"},
                new Location {LocationId = 4, Name = "Japan"},
                new Location {LocationId = 5, Name = "China"},
                new Location {LocationId = 6, Name = "UK"},
                new Location {LocationId = 7, Name = "France"},
                new Location {LocationId = 8, Name = "Brazil"}
            };
        }

        private void InitializeEmployees()
        {
            if (_employees == null)
            {
                Employee e1 = new Employee
                {
                    EmployeeId = 1,
                    LocationId = 4,
                    Password = "password",
                    JobCategoryId = 9,
                    BirthDate = new DateTime(1979, 1, 16),
                    City = "Juneau",
                    Email = "danny.dowling@gmail.com",
                    FirstName = "Danny",
                    LastName = "Dowling",
                    PhoneNumber = "324777888773",
                    Street = "1 Grimoire Place",
                    Zip = "99801",
                    Comment = "Using Fake Address and Phone number here",
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
                typedUser u1 = new typedUser
                {
                    userId = 1,                    
                    Email = "danny.dowling@gmail.com",
                    FirstName = "Danny",
                    LastName = "Dowling",
                    Comment = "Using Fake Address and Phone number here",
                    ExitDate = null,
                    JoinedDate = new DateTime(2019, 3, 1)

                };
                _users = new List<typedUser>() { u1 };
            } 
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await Task.Run(() => Employees);
        }

        public async Task<List<Location>> GetAllCountries()
        {
            return await Task.Run(() => Countries);
        }

        public async Task<List<JobCategory>> GetAllJobCategories()
        {
            return await Task.Run(() => JobCategories);
        }

        public async Task<IEnumerable<typedUser>> GetAllUsers()
        {
            return await Task.Run(() => typedUsers);
        }

        public async Task<typedUser> GetUserById(int userId)
        {
            return await Task.Run(() => { return typedUsers.FirstOrDefault(e => e.userId == userId); });
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
