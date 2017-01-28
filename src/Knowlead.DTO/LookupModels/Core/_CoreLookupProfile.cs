using AutoMapper;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class _CoreLookupProfile : Profile
    {
        public _CoreLookupProfile()
        {
            CreateMap<Achievement, AchievementModel>().ReverseMap();
                  
            CreateMap<FOS, FOSModel>().ReverseMap();

            CreateMap<Language, LanguageModel>().ReverseMap();

            CreateMap<Reward, RewardModel>().ReverseMap();
        }
    }
}
