using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eSusInsurers.Options
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IApiVersionDescriptionProvider _provider;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _provider = provider;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Configure(string? name, SwaggerGenOptions options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Configure(options);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public void Configure(SwaggerGenOptions options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            // add swagger document for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                if (!options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(description.GroupName))
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        CreateVersionInfo(description));
                }
            }

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"eSusInsurers.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, true);

            // add example filters
            options.ExampleFilters();
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "eSusInsurer API",
                Version = description.ApiVersion.ToString(),
                Description = "A RESTful API for managing eSus Insurance Module."
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

    }
}
