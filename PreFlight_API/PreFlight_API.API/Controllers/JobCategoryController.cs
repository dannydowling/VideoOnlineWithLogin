using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreFlight_API.API.Models;
using PreFlight_API.API.Swagger;
using PreFlight_API.BLL.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PreFlight_API.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/jobcategories")]
    [ApiVersion("1.0")]
    [ApiController]
    public class JobCategoryController : Controller
    {
           private readonly IMapper _mapper;
        private readonly IJobCategoryService _jobCategoryService;

        /// <summary>
        /// JobCategory db api
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="jobCategoryService"></param>
        public JobCategoryController(IMapper mapper, IJobCategoryService jobCategoryService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jobCategoryService = jobCategoryService ?? throw new ArgumentNullException(nameof(jobCategoryService));
        }



          /// <summary>
        /// Get JobCategory by id
        /// </summary>
        /// <param name="id">JobCategory Id</param>
        /// <returns>Returns finded jobCategory</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(JobCategory), Description = "Returns finded JobCategory")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(JobCategoryModelExample))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid JobCategory id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetJobCategoryAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _jobCategoryService.GetJobCategoryAsync(id);
            if (result == null)
            {
                return NotFound(new { id });
            }

            return Ok(_mapper.Map<JobCategory>(result));
        }

        
       /// <summary>
        /// Get JobCategories list
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<JobCategory>), Description = "Returns finded JobCategories array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid pageNumber or pageSize")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetJobCategoryListAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if (pageNumber == 0 || pageSize == 0)
            {
                return BadRequest();
            }

            var result = await _jobCategoryService.GetJobCategoryListAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<JobCategory>>(result));
        }
    }
}
