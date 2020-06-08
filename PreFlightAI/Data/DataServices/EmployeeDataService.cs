using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PreFlightAI.Shared.Employee;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace PreFlightAI.Server.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _clientEmployee;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public EmployeeDataService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _clientEmployee = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }
        
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                (await _clientEmployee.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesByLocationId(int locationId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                (await _clientEmployee.GetStreamAsync($"api/employee/{locationId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>
                (await _clientEmployee.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Employee?> AddEmployee(Employee employee)
        {
            var employeeJson =
                new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await _clientEmployee.PostAsync("api/employee", employeeJson);

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

            await _clientEmployee.PutAsync("api/employee", employeeJson);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _clientEmployee.DeleteAsync($"api/employee/{employeeId}");
        }
    }
}
