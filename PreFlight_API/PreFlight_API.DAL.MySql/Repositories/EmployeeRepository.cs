using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace PreFlight_API.DAL.MySql
{
    public class EmployeeRepository : IEmployeeRepository, IHealthCheck
    {

        private readonly IOptionsMonitor<PreFlightMySqlRepositoryOption> _options;

        public EmployeeRepository(IOptionsMonitor<PreFlightMySqlRepositoryOption> options)
        {
            _options = options;
        }

        public async Task<EmployeeEntity> CreateEmployeeAsync(EmployeeEntity newEmployee)
        {
            if (newEmployee.Id == Guid.Empty.ToString())
            {
                newEmployee.Id = Guid.NewGuid().ToString();
            }

            var dnow = DateTime.UtcNow;
            newEmployee.RowVersion = dnow;
            newEmployee.JoinedDate = dnow;

            const string sqlQuery = @"INSERT INTO employees (
                    id,
                    Email,
                    FirstName,
                    LastName,
                    BirthDate,
                    JobCategoryId,
                    PhoneNumber,
                    employeeLocations,
                    JoinedDate,
                    employeeWeatherList,
                    Password,
                    RowVersion
                )
                VALUES (                    
                  @id,
                  @Email,
                  @FirstName,
                  @LastName,
                  @BirthDate,
                  @JobCategoryId,
                  @PhoneNumber,
                  @employeeLocations,
                  @JoinedDate,
                  @employeeWeatherList,
                  @Password,
                  @RowVersion);";

            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, newEmployee, commandType: CommandType.Text);
                return newEmployee;
            }
        }

        public async Task<EmployeeEntity> GetEmployeeAsync(Guid id)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDBConnectionString))
            {
                const string sqlQuery = @"SELECT
                      id,
                      Email,
                      FirstName,
                      LastName,
                      BirthDate,
                      JobCategoryId,
                      PhoneNumber,
                      employeeLocations,
                      JoinedDate,
                      employeeWeatherList,
                      Password,
                      RowVersion
                    FROM employees
                    WHERE id=@id;";
                return await db.QueryFirstOrDefaultAsync<EmployeeEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);

            }
        }
        public async Task<bool> UpdateEmployeeAsync(EmployeeEntity employee)
        {
            employee.RowVersion = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE employees SET                
                id = @id,
                Email = @Email,
                FirstName = @FirstName,
                LastName = @LastName,
                BirthDate = @BirthDate,
                JobCategoryId = @JobCategoryId,
                PhoneNumber = @PhoneNumber,
                employeeLocations = @employeeLocations,
                JoinedDate = @JoinedDate,
                employeeWeatherList = @employeeWeatherList,
                Password = @Password,
                RowVersion = @RowVersion
            WHERE id = @id;";

            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, employee, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            const string sqlQuery = @"DELETE FROM employees WHERE id = @id;";
            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<EmployeeEntity>> GetEmployeeListAsync(int pageNumber, int pageSize)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT
                      id,
                      Email,
                      FirstName
                    FROM employees
                LIMIT @pageSize OFFSET @offset;";
                return await db.QueryAsync<EmployeeEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                try
                {
                    db.Open();
                    db.Close();
                    return Task.FromResult(HealthCheckResult.Healthy());
                }
                catch
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy());
                }
            }
        }
    }
}