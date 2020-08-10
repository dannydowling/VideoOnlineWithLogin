using PreFlight_API.DAL.MySql.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight_API.DAL.MySql.Contract
{
    
    public interface IJobCategoryRepository
    {
        Task<IEnumerable<JobCategoryEntity>> GetJobCategoryListAsync(int pageNumber, int pageSize);
        Task<JobCategoryEntity> GetJobCategoryAsync(Guid id);
    }
}
