using AutoMapper;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DTO.UserModels
{
    public class _UserProfile : Profile
    {
        public _UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserModel>();
            CreateMap<ApplicationUserModel, ApplicationUser>();

            CreateMap<RegisterUserModel, ApplicationUser>();

        }
    }
}
