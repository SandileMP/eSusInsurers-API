using AutoMapper;
using eSusInsurers.Common.Mappings;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;

namespace eSusInsurers.Models
{
    public class UserRegisterRequest : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public int UserTypeId { get; set; }

        public int? InsurerUserId { get; set; }

        public bool IsEnforcePassword { get; set; }

        public string CreatedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRegisterRequest, User>()
                .ForMember(x => x.UserTypeId, opt => opt.MapFrom(c => c.UserTypeId))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName))
                .ForMember(x => x.InsurerUserId, opt => opt.MapFrom(c => c.InsurerUserId.HasValue ? c.InsurerUserId : null))
                .ForMember(x => x.IsEnforcePassword, opt => opt.MapFrom(c => IsEnforcePassword))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => true))
                .ForMember(x => x.PasswordSalt, opt => opt.MapFrom(c => PasswordHasher.GenerateSalt()))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(c => c.CreatedBy))
                .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(c => c.CreatedBy))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(c => DateTime.UtcNow))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(c => DateTime.UtcNow));
        }
    }
}
