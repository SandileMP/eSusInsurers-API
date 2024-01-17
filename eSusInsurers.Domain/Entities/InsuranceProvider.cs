using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSusInsurers.Domain.Entities
{
    public partial class InsuranceProvider : BaseAuditableEntity
    {
        public string InsurerName { get; set; }

        public string TaxIdentificationNumber { get; set; }

        public string ContactPersonName { get; set; }

        public decimal ContactPersonMobileNumber { get; set; }

        public decimal? ContactPersonAlternateContactNumber { get; set; }

        public string ContactPersonEmailId { get; set; }

        public string ContactPersonAlternateEmailId { get; set; }

        public string Country { get; set; }

        public int? CountryCodeId { get; set; }

        public string HeadOfficeAddress { get; set; }

        public string EmailId1 { get; set; }

        public string EmailId2 { get; set; }

        public decimal ContactNumber1 { get; set; }

        public decimal? ContactNumber2 { get; set; }

        public string Password { get; set; }

        public bool IsEnforcePassword { get; set; }

        public string Status { get; set; }

        public bool IsInsurerVerified { get; set; }

        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public string LastVersion { get; set; }

        public bool IsLoggedIn { get; set; }

        public int LoginCount { get; set; }

        public string ProfilePicture { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public string Comments { get; set; }

        public string Address { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<InsuranceProviderDocument> InsuranceProviderDocuments { get; set; } = new List<InsuranceProviderDocument>();
    }
}
