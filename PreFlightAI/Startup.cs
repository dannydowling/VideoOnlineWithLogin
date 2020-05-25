using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PreFlightAI.Api.Models;
using PreFlightAI.Server.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using PreFlight.AI.Server.Services.HttpClients;
using Serilog;
using Serilog.Events;
using PreFlight.AI.Server.Services.SQL;


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
            var logger = new LoggerConfiguration()
                          .MinimumLevel.Debug()
                          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                          .Enrich.FromLogContext()
                          .WriteTo.File("ServerLog.Txt");
            Log.Logger = logger.CreateLogger();
            Log.Information("server service is started.");

            services.AddDbContext<ServerDbContext>();
            services.AddDbContext<IDPContext>();

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<MessageModel>();            

            services.AddHttpClient<MessageModel>(clientMessaging =>
            {
                clientMessaging.BaseAddress = new Uri("https://localhost:46633");
                clientMessaging.DefaultRequestHeaders.Clear();
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));


            services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(clientEmployee =>
            {
                clientEmployee.BaseAddress = new Uri("https://localhost:46633");
                clientEmployee.DefaultRequestHeaders.Clear();
                clientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));


            services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(clientJobcategory =>
            {
                clientJobcategory.BaseAddress = new Uri("https://localhost:46633");
                clientJobcategory.DefaultRequestHeaders.Clear();
                clientJobcategory.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));


            services.AddHttpClient<ILocationDataService, LocationDataService>(clientLocation =>
            {
                clientLocation.BaseAddress = new Uri("https://localhost:46633");
                clientLocation.DefaultRequestHeaders.Clear();
                clientLocation.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip });


            services.AddHttpClient<IUserDataService, UserDataService>(clientUser =>
            {
                clientUser.BaseAddress = new Uri("https://localhost:46633");
                clientUser.DefaultRequestHeaders.Clear();
                clientUser.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip });


            services.AddHttpClient<IWeatherDataService, WeatherDataService>(clientWeather =>
            {
                clientWeather.BaseAddress = new Uri("https://localhost:46633");
                clientWeather.DefaultRequestHeaders.Clear();
                clientWeather.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip });

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
