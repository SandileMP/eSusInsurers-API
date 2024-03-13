namespace eSusInsurers.Models.InsuranceProviders.CreateInsuranceProvider
{
    public class InsuranceProviderFiles
    {
        /// <summary>
        /// The document name of the service provider
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Document Data in Base 64
        /// </summary>
        public string DocumentData { get; set; }
    }
}
