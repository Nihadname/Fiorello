using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication11.Data;
using WebApplication11.Repositories.interfaces;
using WebApplication11.Repositories;
using WebApplication11.Services;
using WebApplication11.Services.interfaces;

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
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<ISumService, SumService>();
            //services.AddTransient<ISumService, SumService>();
            //services.AddSingleton<ISumService, SumService>();
        }

    }
}
