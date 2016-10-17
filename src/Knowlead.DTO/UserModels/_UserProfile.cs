using AutoMapper;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class _UserProfile : Profile
    {
        public _UserProfile()
        {
            CreateMap<RegisterUserModel, ApplicationUser>();

            CreateMap<ApplicationUser, ApplicationUserModel>()
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.ApplicationUserLanguages));

            CreateMap<ApplicationUserLanguage, LanguageModel>()
                .ForMember(dest => dest.CoreLookupId, opt => opt.MapFrom(src => src.LanguageId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Language.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Language.Name));
        }
    }
}
