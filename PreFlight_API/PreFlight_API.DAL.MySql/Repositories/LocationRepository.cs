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
    public class LocationRepository : ILocationRepository, IHealthCheck
    {

        private readonly IOptionsMonitor<PreFlightMySqlRepositoryOption> _options;

        public LocationRepository(IOptionsMonitor<PreFlightMySqlRepositoryOption> options)
        {
            _options = options;
        }

        public async Task<LocationEntity> CreateLocationAsync(LocationEntity newLocation)
        {
            if (newLocation.Id == Guid.Empty.ToString())
            {
                newLocation.Id = Guid.NewGuid().ToString();
            }

            const string sqlQuery = @"INSERT INTO locations (
                    id,
                    Name,
                    Street,
                    Zip,
                    City,
                    Country
                )
                VALUES (
                    @id,
                    @Name,
                    @Street,
                    @Zip,
                    @Country);";

            using (var db = new MySqlConnection(_options.CurrentValue.LocationDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, newLocation, commandType: CommandType.Text);
                return newLocation;
            }
        }

        public async Task<LocationEntity> GetLocationAsync(Guid id)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.LocationDbConnectionString))
            {
                const string sqlQuery = @"SELECT
                      id,
                      Name,
                      Street,
                      Zip,
                      City,
                      Country
                    FROM locations
                    WHERE id=@id;";
                return await db.QueryFirstOrDefaultAsync<LocationEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);

            }
        }
        public async Task<bool> UpdateLocationAsync(LocationEntity location)
        {           
            const string sqlQuery = @"UPDATE locations SET                
                  Name = @Name,
                  Street = @Street,
                  Zip = @Zip,
                  City = @City,
                  Country = @Country
            WHERE id = @id;";

            using (var db = new MySqlConnection(_options.CurrentValue.LocationDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, location, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteLocationAsync(LocationEntity location)
        {
            const string sqlQuery = @"DELETE FROM locations WHERE id = @id;";
            using (var db = new MySqlConnection(_options.CurrentValue.LocationDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { id = location.Id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<LocationEntity>> GetLocationListAsync(int pageNumber, int pageSize)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.LocationDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT
                        id,
                        Name,
                        Street,
                        Zip,
                        City,
                        Country
                    FROM locations
                LIMIT @pageSize OFFSET @offset;";
                return await db.QueryAsync<LocationEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var db = new MySqlConnection(_options.CurrentValue.LocationDbConnectionString))
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
