using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PreFlightAI.Shared;

namespace PreFlightAI.Server.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _clientJobcategory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public JobCategoryDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _clientJobcategory = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }
        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
              return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>
                (await _clientJobcategory.GetStreamAsync($"api/jobcategory"), 
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId)
        {
            return await JsonSerializer.DeserializeAsync<JobCategory>
                (await _clientJobcategory.GetStreamAsync($"api/jobcategory/{jobCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
