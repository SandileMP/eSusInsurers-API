using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using eSusInsurers.Policy;

namespace eSusInsurers.ConfigServices
{
    /// <summary>
    /// Helper class for configuring controller-related services.
    /// </summary>
    public static class ControllerHelper
    {
        /// <summary>
        /// Adds controller-related services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddControllerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddSerializerConfiguration()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // Replace the built-in ASP.NET InvalidModelStateResponse to use our custom response code with 422
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState, statusCode: 422);
                        var result = new UnprocessableEntityObjectResult(problemDetails);
                        result.ContentTypes.Add("application/problem+json");
                        return result;
                    };
                })
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
                });

            //services.AddSingleton<ICurrentUserService, CurrentUserService>();
            //services.AddSingleton<IIdentityService, IdentityService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
