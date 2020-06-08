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
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;

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

         
            services.AddControllersWithViews()
                 .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddHttpContextAccessor();
            services.AddTransient<TokenBearerHandler>();

           

            services.AddRazorPages();
            services.AddServerSideBlazor();

            #region Identity Services

            //Add Open ID Authentication into the pipeline
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
               
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
               {
                   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = "https://localhost:5001";
                   
                   options.ClientId = "IDPClient";
                   options.ResponseType = "code";   
                   
                   options.Scope.Add("roles");
                   options.Scope.Add("PreFlight");
                   options.Scope.Add("offline_access");

                   options.ClaimActions.DeleteClaim("sid");
                   options.ClaimActions.DeleteClaim("idp");
                   options.ClaimActions.DeleteClaim("s_hash");
                   options.ClaimActions.DeleteClaim("auth_time");

                   options.ClaimActions.MapUniqueJsonKey("role", "role");

                   options.SaveTokens = true;
                   options.UsePkce = true;
                                      
                   options.ClientSecret = "IT_DANNY";
                   options.GetClaimsFromUserInfoEndpoint = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = JwtClaimTypes.GivenName,
                       RoleClaimType = JwtClaimTypes.Role
                   };
               });

            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            #endregion

            #region ChatApp
            services.AddSignalR();
            services.AddScoped<MessageModel>();
            services.AddHttpClient<MessageModel>(clientMessaging =>
            {
                clientMessaging.BaseAddress = new Uri("https://localhost:44336");
                clientMessaging.DefaultRequestHeaders.Clear();
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));

            #endregion

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(clientEmployee =>
            {
                clientEmployee.BaseAddress = new Uri("https://localhost:44336");
                clientEmployee.DefaultRequestHeaders.Clear();
                clientEmployee.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                                                .AddHttpMessageHandler<TokenBearerHandler>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHttpClient<IUserDataService, UserDataService>(clientUser =>
            {
                clientUser.BaseAddress = new Uri("https://localhost:44336");
                clientUser.DefaultRequestHeaders.Clear();
                clientUser.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                      .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                      { AutomaticDecompression = System.Net.DecompressionMethods.GZip })
                      .AddHttpMessageHandler<TokenBearerHandler>();


            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(clientJobcategory =>
            {
                clientJobcategory.BaseAddress = new Uri("https://localhost:44336");
                clientJobcategory.DefaultRequestHeaders.Clear();
                clientJobcategory.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)));


            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddHttpClient<ILocationDataService, LocationDataService>(clientLocation =>
            {
                clientLocation.BaseAddress = new Uri("https://localhost:44336");
                clientLocation.DefaultRequestHeaders.Clear();
                clientLocation.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip })
                        .AddHttpMessageHandler<TokenBearerHandler>(); 


            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddHttpClient<IWeatherDataService, WeatherDataService>(clientWeather =>
            {
                clientWeather.BaseAddress = new Uri("https://localhost:44336");
                clientWeather.DefaultRequestHeaders.Clear();
                clientWeather.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler(handler => new RetryPolicy(2, TimeSpan.FromSeconds(20)))
                        .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                        { AutomaticDecompression = System.Net.DecompressionMethods.GZip })
                        .AddHttpMessageHandler<TokenBearerHandler>();


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
                
                //endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/Chat");
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

            });

        }
    }
}
