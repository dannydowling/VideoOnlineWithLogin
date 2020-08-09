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
    public class UserRepository : IUserRepository, IHealthCheck
    {
        private readonly IOptionsMonitor<PreFlightMySqlRepositoryOption> _options;

        public UserRepository(IOptionsMonitor<PreFlightMySqlRepositoryOption> options)
        {
            _options = options;
        }

        public async Task<UserModelEntity> AddUserAsync(UserModelEntity newUser)
        {
            if (newUser.Id == Guid.Empty.ToString())
            {
                newUser.Id = Guid.NewGuid().ToString();
            }

            var dnow = DateTime.UtcNow;
            newUser.RowVersion = dnow;
            newUser.JoinedDate = dnow;

            const string sqlQuery = @"INSERT INTO users (
                    id,
                    FirstName,
                    LastName,
                    Email,
                    Comment,
                    RowVersion,
                    JoinedDate,
                    ExitDate,
                    Weathers,
                    Password                    
                )
                VALUES (
                     @id,
                     @FirstName,
                     @LastName,
                     @Email,
                     @Comment,
                     @RowVersion,
                     @JoinedDate,
                     @ExitDate,
                     @Weathers,
                     @Password);";

            using (var db = new MySqlConnection(_options.CurrentValue.UserDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, newUser, commandType: CommandType.Text);
                return newUser;
            }
        }


        public async Task<UserModelEntity> GetUserByIdAsync(Guid id)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.UserDbConnectionString))
            {
                const string sqlQuery = @"SELECT
                         id,
                         FirstName,
                         LastName,
                         Email,
                         Comment,
                         RowVersion,
                         JoinedDate,
                         ExitDate,
                         Weathers,
                         Password
                         FROM users
                         WHERE id=@id;";
                return await db.QueryFirstOrDefaultAsync<UserModelEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);

            }
        }
        public async Task<bool> UpdateUserAsync(UserModelEntity user)
        {
            user.RowVersion = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE users SET                
                id = @id,                
                FirstName = @FirstName,
                LastName =  @LastName,
                Email = @Email,
                Comment = @Comment,
                RowVersion = @RowVersion,
                JoinedDate = @JoinedDate,
                ExitDate = @ExitDate,
                Weathers = @Weathers,
                Password = @Password
            WHERE id = @id;";

            using (var db = new MySqlConnection(_options.CurrentValue.UserDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, user, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            const string sqlQuery = @"DELETE FROM users WHERE id = @id;";
            using (var db = new MySqlConnection(_options.CurrentValue.UserDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<UserModelEntity>> GetAllUserListAsync(int pageNumber, int pageSize)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.UserDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT
                       id,
                       FirstName,
                       LastName,
                       Email,
                       Comment,
                       RowVersion,
                       JoinedDate,
                       ExitDate,
                       Weathers
                    FROM users
                LIMIT @pageSize OFFSET @offset;";
                return await db.QueryAsync<UserModelEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var db = new MySqlConnection(_options.CurrentValue.UserDbConnectionString))
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
