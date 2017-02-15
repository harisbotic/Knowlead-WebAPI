using AutoMapper;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using Knowlead.DTO.LookupModels.FeedbackModels;

namespace Knowlead.DTO.FeedbackModels
{
    public class _FeedbackProfile : Profile
    {
        public _FeedbackProfile()
        {
            CreateMap<P2PFeedbackModel, P2PFeedback>()
                .ForMember(src => src.FeedbackText, opt => opt.MapFrom(src => src.FeedbackText))
                .ForMember(src => src.Expertise, opt => opt.MapFrom(src => src.Expertise))
                .ForMember(src => src.Helpful, opt => opt.MapFrom(src => src.Helpful))
                .ForMember(src => src.P2pId, opt => opt.MapFrom(src => src.P2pId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<P2PFeedback, P2PFeedbackModel>();

        }
    } 
}
