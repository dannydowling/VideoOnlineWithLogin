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
    [Route("api/v{version:apiVersion}/weather")]
    [ApiVersion("1.0")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// Weather db api
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="weatherService"></param>
        public WeatherController(IMapper mapper, IWeatherService weatherService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        /// <summary>
        /// Create new weather
        /// </summary>
        /// <param name="weather"></param>
        /// <returns>Returns created car</returns>           
        [HttpPost("")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(WeatherModelExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(Weather), Description = "Returns created weather")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> CreateWeatherAsync([FromBody] Weather weather)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _weatherService.CreateWeatherAsync(_mapper.Map<BLL.Models.Weather>(weather));
            return Created($"{result.Id}", _mapper.Map<Weather>(result));
        }

        /// <summary>
        /// Get weather by id
        /// </summary>
        /// <param name="id">Weather Id</param>
        /// <returns>Returns finded weather</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Weather), Description = "Returns finded weather")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(WeatherModelExample))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid weather id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetWeatherAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _weatherService.GetWeatherAsync(id);
            if (result == null)
            {
                return NotFound(new { id });
            }

            return Ok(_mapper.Map<Weather>(result));
        }

        /// <summary>
        /// Update existing weather
        /// </summary>
        /// <param name="id">Weather id</param>
        /// <param name="weather">Weather parameters</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Weather), typeof(WeatherModelExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid weather object")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> UpdateWeatherAsync([FromRoute] Guid id, [FromBody] Weather weather)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
            {
                return BadRequest();
            }

            weather.Id = id;
            await _weatherService.UpdateWeatherAsync(_mapper.Map<BLL.Models.Weather>(weather));
            return Ok();
        }

        /// <summary>
        /// Delete weather
        /// </summary>
        /// <param name="id">Weather id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid weather id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> DeleteWeatherAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _weatherService.DeleteWeatherAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get weathers list
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// /// <param name="AirPressure">Page number</param>
        /// <param name="Temperature">Page size</param>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Weather>), Description = "Returns finded weathers array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid: pageNumber,  pageSize, AirPressure or Temperature")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetWeatherListAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50, [FromQuery] double? airPressure, [FromQuery] double? temperature)
        {
            if (pageNumber == 0 || pageSize == 0)
            {
                return BadRequest();
            }

            var result = await _weatherService.GetWeatherListAsync(pageNumber, pageSize, airPressure, temperature);
            return Ok(_mapper.Map<IEnumerable<Weather>>(result));
        }
    }
}