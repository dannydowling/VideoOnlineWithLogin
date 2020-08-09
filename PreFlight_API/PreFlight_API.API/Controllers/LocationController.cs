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
    [Route("api/v{version:apiVersion}/locations")]
    [ApiVersion("1.0")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;

        /// <summary>
        /// Location db api
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="locationService"></param>
        public LocationController(IMapper mapper, ILocationService locationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        /// <summary>
        /// Create a new location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Returns created location</returns>           
        [HttpPost("")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(LocationModelExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(Location), Description = "Returns created location")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> CreateLocationAsync([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _locationService.CreateLocationAsync(_mapper.Map<BLL.Models.Location>(location));
            return Created($"{result.Id}", _mapper.Map<Location>(result));
        }

        /// <summary>
        /// Get location by id
        /// </summary>
        /// <param name="id">Location Id</param>
        /// <returns>Returns finded location</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Location), Description = "Returns finded location")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(LocationModelExample))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid location id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetLocationAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _locationService.GetLocationAsync(id);
            if (result == null)
            {
                return NotFound(new { id });
            }

            return Ok(_mapper.Map<Location>(result));
        }

        /// <summary>
        /// Update existing location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <param name="location">Location parameters</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Location), typeof(LocationModelExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid location object")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> UpdateLocationAsync([FromRoute] Guid id, [FromBody] Location location)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
            {
                return BadRequest();
            }

            location.Id = id;
            await _locationService.UpdateLocationAsync(_mapper.Map<BLL.Models.Location>(location));
            return Ok();
        }

        /// <summary>
        /// Delete location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid location id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> DeleteLocationAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _locationService.DeleteLocationAsync(_locationService.GetLocationAsync(id).Result);
            return Ok();
        }

        /// <summary>
        /// Get location list
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Location>), Description = "Returns finded locations array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid pageNumber or pageSize")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetCarsListAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if (pageNumber == 0 || pageSize == 0)
            {
                return BadRequest();
            }

            var result = await _locationService.GetLocationListAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<Location>>(result));
        }
    }
}

