using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlight.AI.Server.Services.HttpClients;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly employeeHttpClient httpClientEmployee;
               
        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>
                (await httpClientEmployee.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId)
        {
            return await JsonSerializer.DeserializeAsync<JobCategory>
                (await httpClientEmployee.GetStreamAsync($"api/jobcategory/{jobCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
