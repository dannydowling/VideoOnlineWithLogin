using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PreFlight.IDP.Areas.Identity.Data;
using PreFlight.AI.IDP.Contexts;
using System;

[assembly: HostingStartup(typeof(PreFlight.AI.IDP.Areas.Identity.IdentityHostingStartup))]
namespace PreFlight.AI.IDP.Areas.Identity
{    
        public class IdentityHostingStartup : IHostingStartup
        {
            public void Configure(IWebHostBuilder builder)
            {
                builder.ConfigureServices((context, services) => {
                    services.AddDbContext<IDPContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("IDPContextConnection")));

                    //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    //    .AddEntityFrameworkStores<UserDbContext>();

                    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                            options.SignIn.RequireConfirmedAccount = true)
                           .AddEntityFrameworkStores<IDPContext>()
                           .AddDefaultTokenProviders();

                    services.AddTransient<IEmailSender, EmailSender>();
                });
            }
        }
    }
