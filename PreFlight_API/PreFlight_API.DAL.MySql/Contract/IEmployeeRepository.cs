using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlight_API.DAL.MySql.Models;

namespace PreFlight_API.DAL.MySql.Contract
{
    public interface IEmployeeRepository
    {
        EmployeeEntity GetEmployeeAsync(Guid id);
        EmployeeEntity CreateEmployeeAsync(EmployeeEntity employee);
        Task<bool> UpdateEmployeeAsync(EmployeeEntity employee);
        Task<bool> DeleteEmployeeAsync(Guid id);

        Task<IEnumerable<EmployeeEntity>> GetEmployeeListAsync(int pageNumber, int pageSize);
    }
}
