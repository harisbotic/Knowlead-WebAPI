using AutoMapper;
using Knowlead.DomainModel.StatisticsModels;

namespace Knowlead.DTO.StatisticsModels
{
    public class _StatisticsProfile : Profile
    {
        public _StatisticsProfile()
        {
            CreateMap<PlatformFeedback, PlatformFeedbackModel>();
        }
    }
}
