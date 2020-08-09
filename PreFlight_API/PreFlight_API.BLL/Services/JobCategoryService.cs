using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;

namespace PreFlightAI.Server.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobCategoryService(IMapper mapper, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories(int currentJobCategory)
        {
            var jobCategories = await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>
                (await _httpClient.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //get the jobcategories that are lower ranked than the current employee's level.
            jobCategories = (IEnumerable<JobCategory>)Enum.GetValues
                (typeof(JobCategory)).Cast<int>().Where(x => x <= currentJobCategory); ;
           
                return _mapper.Map<IEnumerable<JobCategory>>(jobCategories);
        }

        public async Task<JobCategory> GetJobCategoryById(Guid id)
        {
            var jobCategory = await JsonSerializer.DeserializeAsync<JobCategory>
                (await _httpClient.GetStreamAsync($"api/jobcategory/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return _mapper.Map<JobCategory>(jobCategory);
        }
    }
}
