using System.Collections.Generic;
using VideoOnlineWithLogin.Shared;

namespace VideoOnlineWithLogin.Api.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
