using eSusInsurers.Extensions;
using System.Text.Json;

namespace eSusInsurers.Policy
{
    /// <summary>
    /// Policy for converting Pascal case fieldnames into snake naming(snake_case) convention.
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// Convert Pascal naming using snake_case using snake casing convention.
        /// </summary>
        /// <param name="name">input to convert</param>
        /// <returns>snake_case format of input</returns>
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}
