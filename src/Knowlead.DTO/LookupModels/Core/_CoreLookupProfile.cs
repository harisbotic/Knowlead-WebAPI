using AutoMapper;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class _CoreLookupProfile : Profile
    {
        public _CoreLookupProfile()
        {
            CreateMap<Achievement, AchievementModel>();
            CreateMap<AchievementModel, Achievement>();
                  
            CreateMap<FOS, FOSModel>();
            CreateMap<FOSModel, FOS>();

            CreateMap<Language, LanguageModel>();
            CreateMap<LanguageModel, Language>();
           

        }
    }
}
