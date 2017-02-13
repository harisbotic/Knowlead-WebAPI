using AutoMapper;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DTO.NotificationModels;

namespace Knowlead.DTO.UserModels
{
    public class _NotificationProfile : Profile
    {
        public _NotificationProfile()
        {
            CreateMap<Notification, NotificationModel>()
                .ForMember(dest => dest.P2p, opt => opt.MapFrom(x => x.P2p))
                .ForMember(dest => dest.P2pId, opt => opt.MapFrom(x => x.P2pId));


            CreateMap<NotificationModel, Notification>()
                .ForMember(dest => dest.ForApplicationUser, opt => opt.Ignore())
                .ForMember(dest => dest.P2p, opt => opt.Ignore());
        }
    }
}
