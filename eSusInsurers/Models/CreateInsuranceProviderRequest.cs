using AutoMapper;
using eSusInsurers.Common;
using eSusInsurers.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace eSusInsurers.Models
{
    public class CreateInsuranceProviderRequest : IMapFrom<InsuranceProvider>, IExamplesProvider<CreateInsuranceProviderRequest>
    {
        /// <summary>
        /// The name of the insurance provider
        /// </summary>
        public string InsuranceProviderName { get; set; } = default!;

        /// <summary>
        /// The name of the contact person
        /// </summary>
        public string ContactPersonName { get; set; } = default!;

        /// <summary>
        /// The mobile number of the contact person
        /// </summary>
        public decimal ContactPersonMobileNumber { get; set; } = default!;

        /// <summary>
        /// The alternate mobile number of the contact person.
        /// </summary>
        public decimal ContactPersonAlternateContactNumber { get; set; } = default!;

        /// <summary>
        /// The email id of the contact person
        /// </summary>
        public string ContactPersonEmailId { get; set; } = default!;

        /// <summary>
        /// The alternate email id of the contact person
        /// </summary>
        public string ContactPersonAlternateEmailId { get; set; } = default!;

        /// <summary>
        /// The country of the service provider.
        /// </summary>
        public string Country { get; set; } = default!;

        /// <summary>
        /// The country id of the country.
        /// </summary>
        public int? CountryId { get; set; } = default!;

        /// <summary>
        /// The head office address of service provider
        /// </summary>
        public string HeadOfficeAddress { get; set; } = default!;

        /// <summary>
        /// The email id of the service provider
        /// </summary>
        public string EmailId1 { get; set; }

        /// <summary>
        /// The alternate email id of the service provider
        /// </summary>
        public string EmailId2 { get; set; }

        /// <summary>
        /// The contact number of the service provider
        /// </summary>
        public decimal ContactNumber1 { get; set; }

        /// <summary>
        /// The alternate contact number of the service provider
        /// </summary>
        public decimal? ContactNumber2 { get; set; }

        /// <summary>
        /// The logged in user.
        /// </summary>
        public string loggedInUser { get; set; }

        /// <summary>
        /// The file details of service provider
        /// </summary>
        public ICollection<InsuranceProviderFiles>? InsuranceProviderFiles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateInsuranceProviderRequest, InsuranceProvider>()
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(src => true))
                .ForMember(dest => dest.InsurerName, opts => opts.MapFrom(src => src.InsuranceProviderName))
                .ForMember(dest => dest.CreatedBy, opts => opts.MapFrom(src => "test"));
        }

        public CreateInsuranceProviderRequest GetExamples()
        {
            return new CreateInsuranceProviderRequest()
            {
                Country = "Swaziland",
                ContactPersonName = "TestUser",
                ContactPersonMobileNumber = 234566535435,
                ContactPersonEmailId = "testuser@test.com",
                CountryId = 1,
                EmailId1 = "emailId@test.com",
                ContactNumber1 = 243543545,
                HeadOfficeAddress = "test",
                InsuranceProviderName = "test",
                InsuranceProviderFiles = new List<InsuranceProviderFiles>()
                {
                    new InsuranceProviderFiles()
                    {
                        DocumentData = "abcdefdadsf",
                        DocumentName = "test",

                    }
                }
            };
        }
    }
}
