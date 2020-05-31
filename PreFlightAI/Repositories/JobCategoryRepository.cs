using System.Collections.Generic;
using System.Linq;
using PreFlight.AI.Server.Services.SQL;
using PreFlightAI.Shared.Things;

namespace PreFlightAI.Api.Models
{
    public class JobCategoryRepository: IJobCategoryRepository
    {
        private readonly ServerDbContext _appDbContext;

        public JobCategoryRepository(ServerDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<JobCategory> GetAllJobCategories()
        {
            return _appDbContext.JobCategories;
        }

        public JobCategory GetJobCategoryById(int jobCategoryId)
        {
            return _appDbContext.JobCategories.FirstOrDefault(c => c.JobCategoryId == jobCategoryId);
        }
    }
}
