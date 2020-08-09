using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using PreFlight_API.DAL.MySql.Contract;
using System;
using Renci.SshNet;
using PreFlight_API.DAL.MySql.Models;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;

namespace PreFlightAI.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IEmployeeRepository _employeeRepo { get; }

        public IEnumerable<EmployeeEntity> employees { get; private set; }

        // The httpContextAccessor is registered in configure services, then accessible in any class.
        // gives information on the context the user is running in. Such as authenticated...
        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepo, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeeRepo = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }
   
        public EmployeeService()
        {            
        }
        
        public async Task<IEnumerable<Employee>> GetEmployeeListAsync(int pageNumber, int pageSize)
        { 
            try
            {
                employees = await _employeeRepo.GetEmployeeListAsync(pageNumber, pageSize);
            }
            catch (NullReferenceException)
            {
               employees = await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeEntity>>
               (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions()
               { PropertyNameCaseInsensitive = true });
            }
           
               return _mapper.Map<IEnumerable<Employee>>(employees);         
        }

        public async Task<Employee> GetEmployeeAsync(Guid id)
        {
            var employee = await JsonSerializer.DeserializeAsync<Employee>
                (await _httpClient.GetStreamAsync($"api/employee/{id}"), 
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return _mapper.Map<Employee>(employee);
        }



        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/employee", employeeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/employee", employeeJson);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/employee/{id}");
        }
    }
}
