using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace eSusInsurers.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            //services.AddSingleton<ISaveChangesInterceptor, AuditableEntityInterceptor>();

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
                //services.AddDbContextPool<IPrimaryDbContext, InsurancePrimaryDbContext>((sp, options) =>
                //{
                //    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                //    options.UseSqlServer(configuration.GetConnectionString("PrimaryDbConnection"));
                //    //.AddInterceptors(new AzADAuthDbConnInterceptor());
                //});
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMemoryCache();

            return services;
        }
    }
}
