using System.Collections.Generic;
using PreFlightAI.Shared;

namespace PreFlightAI.Api.Models
{
    
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
