using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using PreFlightAI.Shared.Employee;

namespace PreFlightAI.Server.Services
{
    public interface IEmployeeDataService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeDetails(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
