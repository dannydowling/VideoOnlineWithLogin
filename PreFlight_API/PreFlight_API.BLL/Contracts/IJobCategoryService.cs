using PreFlight_API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.BLL.Contracts
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategory>> GetJobCategoryListAsync(int pageNumber, int pageSize);
        Task<JobCategory> GetJobCategoryByIdAsync(Guid id);
    }
}
