using EmailService.Interfaces;
using EmailService.Services;
using eSusInsurers.Common.Logging;
using eSusInsurers.ConfigServices;
using eSusInsurers.Helpers;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using System.Reflection;
using ILogger = Serilog.ILogger;

namespace eSusInsurers
{
    /// <summary>
    /// Configures the services for WebApi
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Add the services in the Service Collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllerServices(configuration)
                .AddRouting()
                .AddEndpointsApiExplorer()
                .AddSwaggerConfiguration();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            return services;
        }

        /// <summary>
        /// Configures the services for ApplicationServices
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddLogging(builder =>
            {
                ILogger logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                services.AddSingleton(logger);
            });

            services.AddScoped(typeof(ILoggerContext<>), typeof(LoggerContext<>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Register services
            services.AddTransient(typeof(IInsuranceProviderService), typeof(InsuranceProviderService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IUpdateNotificationTemplate, UpdateNotificationTemplate>();
            services.AddTransient<IEmailService, Services.Implementations.EmailService>();
            services.AddTransient<FireForget>();

            return services;
        }
    }
}
