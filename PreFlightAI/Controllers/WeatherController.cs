using PreFlightAI.Api.Models;
using PreFlightAI.Shared;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetWeatherById(double AirPressure, double Temperature)
        {
            return Ok(_weatherRepository.GetWeatherById(AirPressure, Temperature));
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
        public IActionResult UpdateWeather([FromBody] double AirPressure, double Temperature)
        {            

            if (AirPressure == double.NaN)
            {
                ModelState.AddModelError("Air Pressure Missing", "Air Pressure shouldn't be empty");
            }

            if (Temperature == double.NaN)
            {
                ModelState.AddModelError("Temperature missing", "Temperature shouldn't be empty");
            }

       

            var weatherToUpdate = _weatherRepository.GetWeatherById(AirPressure, Temperature);

            if (weatherToUpdate == null)
                return NotFound();
            foreach (var item in weatherToUpdate)
            {
                _weatherRepository.UpdateWeather(item);

            }

            return NoContent(); //success
        }

        [HttpDelete("{AirPressure}, {Temperature}")]
        public IActionResult DeleteWeather(double AirPressure, double Temperature)
        {
           

            var weatherToDelete = _weatherRepository.GetWeatherById(AirPressure, Temperature);
            if (weatherToDelete == null)
                return NotFound();

            foreach (var item in weatherToDelete)
            {
                _weatherRepository.DeleteWeather(item.AirPressure, item.Temperature);
            }
            
            return NoContent();//success
        }
    }
}
