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

            CreateMap<ApplicationUserModel, ApplicationUser>()
                .ForMember(dest => dest.ApplicationUserLanguages, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationUserInterests, opt => opt.Ignore())
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.MotherTongue, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore());

            CreateMap<ApplicationUser, ApplicationUserModel>()
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.ApplicationUserLanguages))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(src => src.ApplicationUserInterests));
                
            CreateMap<ApplicationUserLanguage, LanguageModel>()
                .ForMember(dest => dest.CoreLookupId, opt => opt.MapFrom(src => src.LanguageId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Language.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Language.Name));
            
            CreateMap<ApplicationUserInterest, InterestModel>().ReverseMap();
            
            //Probably not needed
            // CreateMap<ApplicationUserInterest, InterestModel>()
            //     .ForMember(dest => dest.FosId, opt => opt.MapFrom(src => src.FosId))
            //     .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars));
        }
    }
}
