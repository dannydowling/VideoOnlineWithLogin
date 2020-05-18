using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PreFlightAI.Areas.Identity;
using PreFlight.AI.Server.Services.SQL;
using PreFlightAI.Api.Models;
using PreFlightAI.Server.Services;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using PreFlight.AI.Server.Http.Services;
using Microsoft.Extensions.Options;
using PreFlight.AI.Server.Services;
using PreFlight.AI.Server.Services.HttpClients;

namespace PreFlightAI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // add loggers           
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("Default", LogLevel.Debug)
                       .AddConsole();


                services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AppDbContext>();
                services.AddRazorPages();
                services.AddServerSideBlazor();
                services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

                services.AddScoped<ILocationRepository, LocationRepository>();
                services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IWeatherRepository, WeatherRepository>();
                services.AddScoped<MessageModel>();

                services.AddScoped<IEmployeeDataService, EmployeeDataService>();
                services.AddScoped<IJobCategoryDataService, JobCategoryDataService>();
                services.AddScoped<ILocationDataService, LocationDataService>();
                services.AddScoped<IUserDataService, UserDataService>();
                services.AddScoped<IWeatherDataService, WeatherDataService>();

                services.AddHttpClient<employeeHttpClient>()
                        .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));

                services.AddHttpClient<messagingHttpClient>()
                .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));

                services.AddHttpClient<positioningHttpClient>()
                .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                           .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                           { AutomaticDecompression = System.Net.DecompressionMethods.GZip });

                services.AddHttpClient<userHttpClient>()
                .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                           .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                           { AutomaticDecompression = System.Net.DecompressionMethods.GZip });

                services.AddHttpClient<weatherHttpClient>()
                .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/Chat");
                endpoints.MapFallbackToPage("/_Host");

            });

        }
    }
}
