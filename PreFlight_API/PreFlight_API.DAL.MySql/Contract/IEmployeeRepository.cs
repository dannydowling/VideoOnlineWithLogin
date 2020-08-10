using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlight_API.DAL.MySql.Models;

namespace PreFlight_API.DAL.MySql.Contract
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> GetEmployeeAsync(Guid id);
        Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity employee);
        Task<bool> UpdateEmployeeAsync(EmployeeEntity employee);
        Task<bool> DeleteEmployeeAsync(Guid id);

        Task<IEnumerable<EmployeeEntity>> GetEmployeeListAsync(int pageNumber, int pageSize);
    }
}
