using eSusFarmInternal.API.Interfaces;
using eSusFarmInternal.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace eSusFarmInternal.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddeSusFarmInternalServices(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddHttpClient("InternaleSusFarmService", client =>
            {
                var baseAddress = configuration["InternaleSusFarmServiceAddress"];
                if (string.IsNullOrWhiteSpace(baseAddress))
                {
                    throw new ArgumentException("The configuration key 'Internal eSusFarm Address' is null or empty.");
                }
                client.BaseAddress = new Uri(baseAddress);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            services.AddSingleton<IInternaleSusFarmService, InternaleSusFarmService>();

            return services;
        }
    }
}
