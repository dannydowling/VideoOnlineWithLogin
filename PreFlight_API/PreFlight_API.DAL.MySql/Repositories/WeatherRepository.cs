using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace PreFlight_API.DAL.MySql
{
    public class WeatherRepository : IWeatherRepository, IHealthCheck
    {

        private readonly IOptionsMonitor<PreFlightMySqlRepositoryOption> _options;

        public WeatherRepository(IOptionsMonitor<PreFlightMySqlRepositoryOption> options)
        {
            _options = options;
        }

        public async Task<WeatherEntity> CreateWeatherAsync(WeatherEntity newWeather)
        {
            if (newWeather.Id == Guid.Empty.ToString())
            {
                newWeather.Id = Guid.NewGuid().ToString();
            }

            var dnow = DateTime.UtcNow;
            newWeather.RowVersion = dnow;

            const string sqlQuery = @"INSERT INTO weathers (
                    id,
                    AirPressure,
                    Temperature,
                    WeightValue,
                    RowVersion
                )
                VALUES (
                    @id,
                    @AirPressure,
                    @Temperature,
                    @WeightValue,
                    @RowVersion);";

            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, newWeather, commandType: CommandType.Text);
                return newWeather;
            }
        }

        public async Task<WeatherEntity> GetWeatherAsync(Guid id)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDbConnectionString))
            {
                const string sqlQuery = @"SELECT
                       id,
                       AirPressure,
                       Temperature,
                       WeightValue,
                       RowVersion
                    FROM weathers
                    WHERE id=@id;";
                return await db.QueryFirstOrDefaultAsync<WeatherEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);

            }
        }
        public async Task<bool> UpdateWeatherAsync(WeatherEntity weather)
        {
            weather.RowVersion = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE weathers SET                
                AirPressure = @AirPressure,                
                Temperature = @Temperature,
                WeightValue = @WeightValue,
                RowVersion = @RowVersion
            WHERE id = @id;";

            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, weather, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteWeatherAsync(Guid id, double AirPressure, double Temperature)
        {
            const string sqlQuery = @"DELETE FROM weathers WHERE id = @id AND AirPressure = @AirPressure AND Temperature = @Temperature;";
            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<WeatherEntity>> GetWeatherListAsync(int pageNumber, int pageSize, double? AirPressure, double? Temperature)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;

                string sqlQuery = "";
                if (Temperature != null)
                {
                    sqlQuery = @"SELECT
                       id,
                       AirPressure,
                       Temperature,
                       WeightValue,
                       RowVersion
                    FROM weathers
                    WHERE Temperature=@Temperature
                LIMIT @pageSize OFFSET @offset;";
                }
                else if (AirPressure != null)
                {
                    sqlQuery = @"SELECT
                       id,
                       AirPressure,
                       Temperature,
                       WeightValue,
                       RowVersion
                    FROM weathers
                    WHERE AirPressure=@AirPressure
                LIMIT @pageSize OFFSET @offset;";
                }
                else if (AirPressure != null && Temperature != null)
                {
                    sqlQuery = @"SELECT
                       id,
                       AirPressure,
                       Temperature,
                       WeightValue,
                       RowVersion
                    FROM weathers
                    WHERE AirPressure=@AirPressure
                    AND Temperature=@Temperature
                LIMIT @pageSize OFFSET @offset;";
                }
                else
                {
                    sqlQuery = @"SELECT
                       id,
                       AirPressure,
                       Temperature,
                       WeightValue,
                       RowVersion
                    FROM weathers
                LIMIT @pageSize OFFSET @offset;";
                }

                return await db.QueryAsync<WeatherEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var db = new MySqlConnection(_options.CurrentValue.WeatherDbConnectionString))
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
