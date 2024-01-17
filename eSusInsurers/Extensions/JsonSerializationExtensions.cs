using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace eSusInsurers.Extensions
{
    /// <summary>
    /// Policy for converting Json.
    /// </summary>
    public static class JsonSerializationExtensions
    {
        private static readonly SnakeCaseNamingStrategy _snakeCaseNamingStrategy
            = new SnakeCaseNamingStrategy();

        private static readonly JsonSerializerSettings _snakeCaseSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = _snakeCaseNamingStrategy
            }
        };

        /// <summary>
        /// Converter to snake casing convention.
        /// </summary>
        /// <param name="instance">input to convert</param>
        /// <returns>snake_case format of input</returns>
        public static string ToSnakeCase<T>(this T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(paramName: nameof(instance));
            }

            return JsonConvert.SerializeObject(instance, _snakeCaseSettings);
        }

        /// <summary>
        /// Converter to snake casing convention.
        /// </summary>
        /// <param name="string">input to convert</param>
        /// <returns>snake_case format of input</returns>
        public static string ToSnakeCase(this string @string)
        {
            if (@string == null)
            {
                throw new ArgumentNullException(paramName: nameof(@string));
            }

            return _snakeCaseNamingStrategy.GetPropertyName(@string, false);
        }
    }
}
