using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PreFlight.AI.Server.Services.HttpClients;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly employeeHttpClient clientEmployee;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobCategoryDataService(employeeHttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            clientEmployee = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            clientEmployee.BaseAddress = new Uri("http://localhost:46633/");
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>
                (await clientEmployee.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId)
        {
            return await JsonSerializer.DeserializeAsync<JobCategory>
                (await clientEmployee.GetStreamAsync($"api/jobcategory/{jobCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
