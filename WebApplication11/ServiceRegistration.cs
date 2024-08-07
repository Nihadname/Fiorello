using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication11.Data;
using WebApplication11.Repositories.interfaces;
using WebApplication11.Repositories;
using WebApplication11.Services;
using WebApplication11.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using WebApplication11.Models;

namespace WebApplication11
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews() ;
            services.AddDbContext<FiorelloDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"))
            );
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
            });
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<ISumService, SumService>();
            //services.AddTransient<ISumService, SumService>();
            //services.AddSingleton<ISumService, SumService>();
           
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<FiorelloDbContext>();

        }

    }
}
