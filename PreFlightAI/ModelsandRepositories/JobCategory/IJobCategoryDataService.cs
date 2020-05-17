﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public interface IJobCategoryDataService
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategories();
        Task<JobCategory> GetJobCategoryById(int jobCategoryId);
    }
}