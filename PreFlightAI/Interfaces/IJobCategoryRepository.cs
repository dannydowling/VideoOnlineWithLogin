using PreFlightAI.Shared.Things;
using System.Collections.Generic;


namespace PreFlightAI.Api.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
