using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreFlight_API.API.Models;
using PreFlight_API.API.Swagger;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.DAL.MySql.Models;
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
    [Route("api/v{version:apiVersion}/Employees")]
    [ApiVersion("1.0")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IMapper mapper, IEmployeeService employeeService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Employee>), Description = "Returns finded employee array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid pageNumber or pageSize")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if (pageNumber == 0 || pageSize == 0)
            {
                return BadRequest();
            }

            var result = await _employeeService.GetEmployeeListAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<Employee>>(result));
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Returns created employee</returns>           
        [HttpPost("")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(EmployeeModelExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(Employee), Description = "Returns created employee")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _employeeService.CreateEmployeeAsync(_mapper.Map<BLL.Models.Employee>(employee));
            return Created($"{result.Id}", _mapper.Map<Employee>(result));
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Returns finded employee</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Employee), Description = "Returns finded employee")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(EmployeeModelExample))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid employee id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetEmployeeAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _employeeService.GetEmployeeAsync(id);
            if (result == null)
            {
                return NotFound(new { id });
            }

            return Ok(_mapper.Map<Employee>(result));
        }

        /// <summary>
        /// Update existing employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="employee">Employee parameters</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Employee), typeof(EmployeeModelExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid car object")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] Guid id, [FromBody] Employee employee)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
            {
                return BadRequest();
            }

            employee.Id = id;
            await _employeeService.UpdateEmployeeAsync(_mapper.Map<BLL.Models.Employee>(employee));
            return Ok();
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid car id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }



        //[HttpGet("{id}")]
        //public IActionResult GetEmployeeById(int id)
        //{
        //    return Ok(_employeeRepository.GetEmployeeById(id));
        //}

        //[HttpPost]
        //public IActionResult CreateEmployee([FromBody] EmployeeEntity employee)
        //{
        //    if (employee == null)
        //        return BadRequest();

        //    if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
        //    {
        //        ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
        //    }

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var createdEmployee = _employeeRepository.AddEmployee(employee);

        //    return Created("employee", createdEmployee);
        //}

        //[HttpPut]
        //public IActionResult UpdateEmployee([FromBody] EmployeeEntity employee)
        //{
        //    if (employee == null)
        //        return BadRequest();

        //    if (employee.FirstName == string.Empty || employee.LastName == string.Empty)
        //    {
        //        ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
        //    }

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var employeeToUpdate = _employeeRepository.GetEmployeeById(employee.EmployeeId);

        //    if (employeeToUpdate == null)
        //        return NotFound();

        //    _employeeRepository.UpdateEmployee(employee);

        //    return NoContent(); //success
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteEmployee(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var employeeToDelete = _employeeRepository.GetEmployeeById(id);
        //    if (employeeToDelete == null)
        //        return NotFound();

        //    _employeeRepository.DeleteEmployee(id);

        //    return NoContent();//success
        //}
    }
}
