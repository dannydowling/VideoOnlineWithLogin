using PreFlight_API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.BLL.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeeListAsync(int pageNumber, int pageSize);
        Task<Employee> GetEmployeeAsync(Guid id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid id);
    }
}
