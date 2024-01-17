namespace eSusInsurers.Domain.Entities
{
    public partial class InsuranceProviderDocument : BaseAuditableEntity
    {
        public int InsuranceProviderId { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public bool? IsActive { get; set; }

        public virtual InsuranceProvider InsuranceProvider { get; set; }
    }
}
