using PreFlight_API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.BLL.Contracts
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategories(int currentJobCategory);
        Task<JobCategory> GetJobCategoryById(Guid id);
    }
}
