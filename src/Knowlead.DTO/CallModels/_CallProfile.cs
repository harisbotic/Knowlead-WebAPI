using AutoMapper;
using Knowlead.DomainModel.CallModels;

namespace Knowlead.DTO.CallModels
{
    public class _CallProfile : Profile
    {
        public _CallProfile()
        {
            CreateMap<_CallModel, _Call>()
                .ForMember(dest => dest.CallerId, opt => opt.MapFrom(src => src.Caller.PeerId))
                .ForMember(dest => dest.Caller, opt => opt.Ignore());

            CreateMap<P2PCallModel, P2PCall>()
                .ForMember(dest => dest.CallerId, opt => opt.MapFrom(src => src.Caller.PeerId))
                .ForMember(dest => dest.Caller, opt => opt.Ignore());

            CreateMap<FriendCallModel, FriendCall>()
                .ForMember(dest => dest.CallerId, opt => opt.MapFrom(src => src.Caller.PeerId))
                .ForMember(dest => dest.Caller, opt => opt.Ignore());
        }
    }
}
