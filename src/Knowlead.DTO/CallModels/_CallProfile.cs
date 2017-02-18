using AutoMapper;
using Knowlead.DomainModel.CallModels;

namespace Knowlead.DTO.CallModels
{
    public class _CallProfile : Profile
    {
        public _CallProfile()
        {
            CreateMap<_CallModel, _Call>();

            CreateMap<P2PCallModel, P2PCall>()
                .IncludeBase<_CallModel, _Call>();

            CreateMap<FriendCallModel, FriendCall>()
                .IncludeBase<_CallModel, _Call>();
        }
    }
}
