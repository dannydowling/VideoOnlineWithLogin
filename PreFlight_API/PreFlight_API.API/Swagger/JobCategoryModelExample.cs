using PreFlight_API.API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace PreFlight_API.API.Swagger
{
    public class JobCategoryModelExample : IExamplesProvider<JobCategory>
    {
        public Employee GetExamples()
        {
            return new JobCategory
            {
                Id = Guid.NewGuid(),
                JobCategoryName = JobCategory.User               
            };
        }
    }
}
