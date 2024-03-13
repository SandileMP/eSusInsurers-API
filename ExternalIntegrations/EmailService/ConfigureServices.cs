using EmailService.Interfaces;
using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services
                                                                   , IConfiguration configuration)
        {

            var emailConfig = configuration.GetSection("EmailConfiguration")
                                           .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddSingleton<IEmailSender, EmailSender>();

            services.Configure<FormOptions>(o => {
                                                    o.ValueLengthLimit = int.MaxValue;
                                                    o.MultipartBodyLengthLimit = int.MaxValue;
                                                    o.MemoryBufferThreshold = int.MaxValue;
                                                });

            return services;
        }
    }
}
