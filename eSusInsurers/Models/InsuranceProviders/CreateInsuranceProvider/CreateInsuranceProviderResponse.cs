namespace eSusInsurers.Models.InsuranceProviders.CreateInsuranceProvider
{
    /// <summary>
    /// Represents the result of a newly created insurance provider.
    /// </summary>
    public class CreateInsuranceProviderResponse
    {
        /// <summary>
        /// Indicates whether the insurance provider was sucessfully created.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The reason the insurance provider was not created.
        /// </summary>
        public string? FailureReason { get; set; }

        /// <summary>
        /// The ID of the newly created part inventory
        /// </summary>
        public int? InsuranceProviderId { get; set; }

    }
}
