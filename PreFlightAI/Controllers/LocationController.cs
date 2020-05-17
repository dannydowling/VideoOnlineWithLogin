using PreFlightAI.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreFlightAI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(_locationRepository.GetAllLocations());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetLocationById(int id)
        {
            return Ok(_locationRepository.GetLocationById(id));
        }
    }
}
