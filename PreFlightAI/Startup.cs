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
using Microsoft.Extensions.Options;
using PreFlightAI.Api.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using PreFlight.AI.IDP.Contexts;
using PreFlight.AI.Shared.Handlers;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
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

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<MessageModel>();

            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme,
               options => {
                   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = "https://localhost:44301";
                   options.ClientId = "IDPClient";
                   options.ClientSecret = "IT_DANNY";
                   options.ResponseType = "code id_token";
                   options.UsePkce = true;
                   options.GetClaimsFromUserInfoEndpoint = true;
                   options.Scope.Add("internalServerCommunication");
                   options.ClaimActions.DeleteClaim("sid");
                   options.ClaimActions.DeleteClaim("idp");
                   options.ClaimActions.DeleteClaim("s_hash");
                   options.ClaimActions.DeleteClaim("auth_time");
                   options.Scope.Add("internalServerCommunication");
                   options.SaveTokens = true;
               });

            services.AddTransient<TokenBearerHandler>();
            services.AddHttpClient<MessageModel>(clientMessaging => {
                clientMessaging.BaseAddress = new Uri("https://localhost:46633");
                clientMessaging.DefaultRequestHeaders.Clear();
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));

            services.AddHttpClient("APIClient", clientAPI =>
            {
                clientAPI.BaseAddress = new Uri("https://localhost:46633/");
                clientAPI.DefaultRequestHeaders.Clear();
                clientAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<TokenBearerHandler>();

            services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(clientEmployee =>
            {
                clientEmployee.BaseAddress = new Uri("https://localhost:44301");
                clientEmployee.DefaultRequestHeaders.Clear();
                clientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                                                .AddHttpMessageHandler<TokenBearerHandler>();

            services.AddHttpClient<IUserDataService, UserDataService>(clientUser =>
            {
                clientUser.BaseAddress = new Uri("https://localhost:44301");
                clientUser.DefaultRequestHeaders.Clear();
                clientUser.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                      .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                      { AutomaticDecompression = System.Net.DecompressionMethods.GZip })
                      .AddHttpMessageHandler<TokenBearerHandler>();


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
                app.UseResponseCompression();
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

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
