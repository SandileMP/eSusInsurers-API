namespace eSusInsurers.ConfigServices
{
    /// <summary>
    /// Helper class for Serializer Helpers.
    /// </summary>
    public static class SerializerHelpers
    {
        /// <summary>
        /// Adds serializer configuration to the MVC builder.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <returns>The updated MVC builder.</returns>
        public static IMvcBuilder AddSerializerConfiguration(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = new CustomNamingPolicy();
            });

            return mvcBuilder;
        }
    }
}
