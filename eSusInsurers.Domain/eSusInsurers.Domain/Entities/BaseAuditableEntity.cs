namespace eSusInsurers.Domain.Entities
{
    public class BaseAuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; } = null!;
        public string ModifiedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
