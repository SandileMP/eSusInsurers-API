using AzureIntegrations.API.Interfaces;
using AzureIntegrations.API.Models;
using AzureIntegrations.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureIntegrations.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAzureIntegrationServices(this IServiceCollection services
                                                                   , IConfiguration configuration)
        {

            services.AddSingleton<StorageConfiguration>(configuration.GetSection("StorageConfiguration").Get<StorageConfiguration>());

            services.AddSingleton<IAzureFileStorageConnection, AzureFileStorageConnection>();

            return services;
        }
    }
}
