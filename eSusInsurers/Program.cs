using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AzureIntegrations.API;
using eSusInsurers.Common;
using eSusInsurers.Domain.Models;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Policy;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var _env = builder.Environment;

// Add services to the container.

var configuration = builder.Services.AddEnvironmentVariables(_env);
builder.Services.AddAzureIntegrationServices(configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IInsuranceProviderService), typeof(InsuranceProviderService));
//builder.Services.AddScoped(typeof(IInsuranceProviderDocumentRepository), typeof(InsuranceProviderDocumentRepository));
//builder.Services.AddScoped(typeof(IInsuranceProviderRepository), typeof(InsuranceProviderRepository));

//Database
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(ILoggerContext<>), typeof(LoggerContext<>));

builder.Services.AddDbContext<esusinsurer_nonprodContext>(
               x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

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
