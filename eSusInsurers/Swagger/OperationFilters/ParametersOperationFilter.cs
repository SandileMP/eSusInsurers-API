using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eSusInsurers.Swagger.OperationFilters
{
    /// <summary>
    /// To Map the input parameters
    /// </summary>
    public class ParametersOperationFilter : IOperationFilter
    {
        /// <summary>
        /// To apply the mappings
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.OperationId != "get_insurance_products")
            {
                return;
            }

            var parameterMappings = new Dictionary<string, string>
            {
                { "manufacturer_code", "ManufacturerCode" },
                { "part_number", "PartNumber"  },
                { "part_desc" , "PartDesc" },
                { "exclude_non_inventory" , "ExcludeNonInventory" },
                { "part_type" , "PartType" },
                { "no_movement_in" , "NoMovementIn" },
                { "bin_number_1" , "BinNumber1" },
                { "bin_number_2" , "BinNumber2" },
                { "bin_number_3" , "BinNumber3" },
                { "page" , "Page" },
                { "page_size" , "PageSize" }
            };

            foreach (var parameter in operation.Parameters)
            {
                if (parameterMappings.TryGetValue(parameter.Name, out var mappedName))
                {
                    parameter.Description = $"Filter the results by {parameter.Name.Replace("_", " ")}";
                    parameter.In = ParameterLocation.Query; // Set the parameter as a query parameter
                }
            }
        }
    }
}
