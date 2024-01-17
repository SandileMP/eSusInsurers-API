namespace eSusInsurers.Common
{
    /// <summary>
    /// Helper class for managing environment variables and configuration.
    /// </summary>
    public static class EnvironmentVariablesHelper
    {
        /// <summary>
        /// Adds environment variables and configuration to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="env">The web host environment.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IConfiguration AddEnvironmentVariables(this IServiceCollection services, IWebHostEnvironment? env)
        {
            string environmentName;
            string? keyVault;
            environmentName = env?.EnvironmentName ?? GetEnvironment();
            if (env == null || string.IsNullOrWhiteSpace(environmentName))
            {
                throw new ArgumentException("Environment variable is missing. Environment Variable name ASPNETCORE_ENVIRONMENT");
            }

            var builder = new ConfigurationBuilder();

            if (!string.IsNullOrWhiteSpace(env.ContentRootPath))
            {
                builder.SetBasePath(env.ContentRootPath);
            }

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (environmentName == "localhost" || environmentName == "local")
            {
                builder.AddJsonFile("appsettings.localsecrets.json", optional: true, reloadOnChange: true);
            }
            else
            {
                builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

                //setup keyvault first
                var builtConfig = builder.Build();

                //Get Keyvault name from environment variable
                //keyVault = builtConfig["ServiceSettings-KeyVaultName"];
                //if (string.IsNullOrEmpty(keyVault))
                //{
                //    keyVault = GetKeyVault();
                //}

                //var uri = new Uri($"https://{keyVault}.vault.azure.net/");
                //builder.AddAzureKeyVault(uri, new DefaultAzureCredential());
            }

            builder.AddEnvironmentVariables();

            return builder.Build();
        }

        /// <summary>
        /// Gets the environment name from the ASPNETCORE_ENVIRONMENT environment variable.
        /// </summary>
        /// <returns>The environment name.</returns> 
        private static string GetEnvironment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return string.IsNullOrWhiteSpace(environment) ? string.Empty : environment;
        }
        private static string GetKeyVault()
        {
            var keyVault = Environment.GetEnvironmentVariable("ServiceSettings-KeyVaultName");
            return string.IsNullOrWhiteSpace(keyVault) ? string.Empty : keyVault;
        }
    }
}
