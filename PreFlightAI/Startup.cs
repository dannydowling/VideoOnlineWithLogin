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
using Serilog;
using Serilog.Events;
using PreFlight.AI.Server.Services.SQL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using PreFlight.AI.Shared.Handlers;
using System.IdentityModel.Tokens.Jwt;
using PreFlight.AI.Shared;


namespace PreFlightAI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
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

            var serverConnectionString = Configuration["ConnectionStrings:ServerDBConnectionString"];
            services.AddDbContext<ServerDbContext>(o => o.UseSqlServer(serverConnectionString));

            services.AddControllers();
            services.AddSignalR();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();

            //Add these services to the dependency injection container
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<MessageModel>();


            //Add Open ID Authentication into the pipeline
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
               options =>
               {
                   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = "https://localhost:5001";
                   options.ClientId = "IDPClient";
                   options.ClientSecret = "IT_DANNY";
                   options.ResponseType = "code id_token";
                   options.UsePkce = true;
                   options.GetClaimsFromUserInfoEndpoint = true;

                   options.SaveTokens = true;
               });

            services.AddTransient<TokenBearerHandler>();

            //Add HttpClients for use by the controllers

            services.AddHttpClient<MessageModel>(clientMessaging =>
            {
                clientMessaging.BaseAddress = new Uri("https://localhost:44336");
                clientMessaging.DefaultRequestHeaders.Clear();
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));


            services.AddHttpClient("APIClient", clientAPI =>
            {
                clientAPI.BaseAddress = new Uri("https://localhost:44336/");
                clientAPI.DefaultRequestHeaders.Clear();
                clientAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<TokenBearerHandler>();

            services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(clientEmployee =>
            {
                clientEmployee.BaseAddress = new Uri("https://localhost:44336");
                clientEmployee.DefaultRequestHeaders.Clear();
                clientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                                                .AddHttpMessageHandler<TokenBearerHandler>();

            services.AddHttpClient<IUserDataService, UserDataService>(clientUser =>
            {
                clientUser.BaseAddress = new Uri("https://localhost:44336");
                clientUser.DefaultRequestHeaders.Clear();
                clientUser.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                      .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                      { AutomaticDecompression = System.Net.DecompressionMethods.GZip })
                      .AddHttpMessageHandler<TokenBearerHandler>();


            services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(clientJobcategory =>
            {
                clientJobcategory.BaseAddress = new Uri("https://localhost:44336");
                clientJobcategory.DefaultRequestHeaders.Clear();
                clientJobcategory.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));


            services.AddHttpClient<ILocationDataService, LocationDataService>(clientLocation =>
            {
                clientLocation.BaseAddress = new Uri("https://localhost:44336");
                clientLocation.DefaultRequestHeaders.Clear();
                clientLocation.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip });


            services.AddHttpClient<IWeatherDataService, WeatherDataService>(clientWeather =>
            {
                clientWeather.BaseAddress = new Uri("https://localhost:44336");
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
                app.UseResponseCompression();
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
                endpoints.MapDefaultControllerRoute()
                         .RequireAuthorization();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/Chat");
                endpoints.MapFallbackToPage("/_Host");

            });

        }
    }
}
