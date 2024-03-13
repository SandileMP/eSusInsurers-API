using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eSusInsurers.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                services.AddDbContext<esusinsurer_nonprodContext>(x => x.UseSqlServer(configuration.GetConnectionString("DbConnection"))
                                                                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                                                                        .EnableSensitiveDataLogging());

            }
            else
            {
                services.AddDbContext<esusinsurer_nonprodContext>(x => x.UseSqlServer(configuration.GetConnectionString("DbConnection"))
                                                                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                                                                        .EnableSensitiveDataLogging());
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMemoryCache();

            return services;
        }
    }
}
