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
    public class JobCategoryRepository : IJobCategoryRepository, IHealthCheck
    {

        private readonly IOptionsMonitor<PreFlightMySqlRepositoryOption> _options;

        public JobCategoryRepository(IOptionsMonitor<PreFlightMySqlRepositoryOption> options)
        {
            _options = options;
        }

        public async Task<JobCategoryEntity> GetJobCategoryAsync(Guid id)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                const string sqlQuery = @"SELECT
                       id,
                       JobCategoryName
                    FROM jobcategories
                    WHERE id=@id;";
                return await db.QueryFirstOrDefaultAsync<JobCategoryEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);

            }
        }

        public async Task<IEnumerable<JobCategoryEntity>> GetJobCategoryListAsync(int pageNumber, int pageSize)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.EmployeeDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT
                       id,
                        JobCategoryName
                    FROM jobcategories
                LIMIT @pageSize OFFSET @offset;";
                return await db.QueryAsync<JobCategoryEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
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