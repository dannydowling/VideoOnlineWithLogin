using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System;
using PreFlight.AI.Server.Services.HttpClients;

namespace PreFlightAI.Server.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly employeeHttpClient clientEmployee;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public EmployeeDataService(employeeHttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            clientEmployee = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            clientEmployee.BaseAddress = new Uri("http://localhost:46633/");
        }
        
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                (await clientEmployee.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>
                (await clientEmployee.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await clientEmployee.PostAsync("api/employee", employeeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            await clientEmployee.PutAsync("api/employee", employeeJson);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await clientEmployee.DeleteAsync($"api/employee/{employeeId}");
        }
    }
}
