using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using PreFlight_API.BLL;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql;
using PreFlight_API.DAL.MySql.Contract;
using PreFlightAI.Server.Services;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebServiceExtensions
    {
        public static IServiceCollection AddWebServices(
            this IServiceCollection services,
            IConfigurationSection BLLOptionsSection,
            IConfigurationSection DALOptionSection)
        {
            if (BLLOptionsSection == null)
            {
                throw new ArgumentNullException(nameof(BLLOptionsSection));
            }

            if (DALOptionSection == null)
            {
                throw new ArgumentNullException(nameof(DALOptionSection));
            }

            var bllSettings = BLLOptionsSection.Get<PreFlightBLLOptions>();

            services.Configure<PreFlightBLLOptions>(opt =>
            {
                opt.JwtSecretKey = BLLOptionsSection.GetValue<string>("JwtSecretKey");
                opt.WebApiUrl = BLLOptionsSection.GetValue<string>("WebApiUrl");
            });
            services.Configure<PreFlightMySqlRepositoryOption>(opt =>
            {
                 opt.UserDbConnectionString = DALOptionSection.GetValue<string>("UserDbConnectionString");
                 opt.WeatherDbConnectionString = DALOptionSection.GetValue<string>("WeatherDbConnectionString");
                 opt.EmployeeDbConnectionString = DALOptionSection.GetValue<string>("EmployeeDbConnectionString");
                 opt.LocationDbConnectionString = DALOptionSection.GetValue<string>("LocationDbConnectionString");

            });

            services.TryAddSingleton<IUserRepository, UserRepository>();
            services.TryAddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.TryAddSingleton<IWeatherRepository, WeatherRepository>();
            services.TryAddSingleton<ILocationRepository, LocationRepository>();

            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IEmployeeService, EmployeeService>();
            services.TryAddScoped<IJobCategoryService, JobCategoryService>();
            services.TryAddScoped<ILocationService, LocationService>();
            services.TryAddScoped<IWeatherService, WeatherService>();
            services.TryAddScoped<IJwtTokenService, JwtTokenService>();

            services.AddHttpClient();
            services.AddHttpClient<TodosMockProxyService>(c =>
            {
                c.BaseAddress = new Uri(bllSettings.WebApiUrl);
            }).AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600))
            );

            services.AddHealthChecks()
                .AddCheck<UserRepository>("UserRepository")
                .AddCheck<EmployeeRepository>("EmployeeRepository")
                .AddCheck<WeatherRepository>("WeatherRepository")
                .AddCheck<LocationRepository>("LocationRepository");
                //.AddCheck<TodosMockProxyService>("TodosMockProxyService");

            return services;
        }

        public static IApplicationBuilder UseWebServices(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/health", new HealthCheckOptions()
            {
                ResponseWriter = (httpContext, result) =>
                {
                    httpContext.Response.ContentType = "application/json";

                    var json = new JObject(
                        new JProperty("status", result.Status.ToString()),
                        new JProperty("results", new JObject(result.Entries.Select(pair =>
                            new JProperty(pair.Key, new JObject(
                                new JProperty("status", pair.Value.Status.ToString())))))));
                    return httpContext.Response.WriteAsync(
                        json.ToString(Formatting.Indented));
                }
            });

            return app;
        }
    }
}
