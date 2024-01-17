using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AzureIntegrations.API;
using eSusFarmInternal.API;
using eSusInsurers;
using eSusInsurers.ConfigServices;
using eSusInsurers.Infrastructure;
using eSusInsurers.Options;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var _env = builder.Environment;

// Add services to the container.

var configuration = builder.Services.AddEnvironmentVariables(_env);

builder.Services.AddWebApiServices(configuration)
                .AddApplicationServices(configuration)
                .AddInfrastructureServices(configuration, _env)  
                .AddAzureIntegrationServices(configuration)
                .AddeSusFarmInternalServices(configuration);

var apiVersioningBuilder = builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(
    new QueryStringApiVersionReader("api-version"),
    new HeaderApiVersionReader("api-version"),
    new MediaTypeApiVersionReader("ver"));
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddSwaggerExamplesFromAssemblies(typeof(Program).Assembly);

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
