using PreFlightAI.Api.Models;
using PreFlightAI.Shared;
using Microsoft.AspNetCore.Mvc;
using PreFlightAI.Shared.Places;

namespace PreFlightAI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : Controller
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet]
        public IActionResult GetForecast()
        {
            return Ok(_weatherRepository.GetForecast());
        }

        [HttpGet("{id}")]
        public IActionResult GetWeatherById(int id)
        {
            return Ok(_weatherRepository.GetWeatherById(id));
        }

        [HttpPost]
        public IActionResult CreateWeather([FromBody] Weather weather)
        {
            if (weather == null)
                return BadRequest();

            if (weather.AirPressure == double.NaN)
            {
                ModelState.AddModelError("Air Pressure Missing", "Air Pressure shouldn't be empty");
            }

            if (weather.Temperature == double.NaN)
            {
                ModelState.AddModelError("Temperature missing", "Temperature shouldn't be empty");
            }

            if (weather.WeightValue == double.NaN)
            {
                ModelState.AddModelError("Name missing", "Name shouldn't be empty");
            }          

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdWeather = _weatherRepository.AddWeather(weather);

            return Created("weather", createdWeather);
        }

        [HttpPut]
        public IActionResult UpdateWeather([FromBody] Weather weather)
        {
            if (weather == null)
                return BadRequest();

            if (weather.AirPressure == double.NaN)
            {
                ModelState.AddModelError("Air Pressure Missing", "Air Pressure shouldn't be empty");
            }

            if (weather.Temperature == double.NaN)
            {
                ModelState.AddModelError("Temperature missing", "Temperature shouldn't be empty");
            }

            if (weather.WeightValue == double.NaN)
            {
                ModelState.AddModelError("Name missing", "Name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var weatherToUpdate = _weatherRepository.GetWeatherById(weather.weatherID);

            if (weatherToUpdate == null)
                return NotFound();

            _weatherRepository.UpdateWeather(weather);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWeather(int id)
        {
            if (id == 0)
                return BadRequest();

            var weatherToDelete = _weatherRepository.GetWeatherById(id);
            if (weatherToDelete == null)
                return NotFound();

            _weatherRepository.DeleteWeather(id);

            return NoContent();//success
        }
    }
}
