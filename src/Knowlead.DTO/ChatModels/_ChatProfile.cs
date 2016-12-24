using AutoMapper;
using Knowlead.DomainModel.ChatModels;

namespace Knowlead.DTO.ChatModels
{
    public class _ChatProfile : Profile
    {
        public _ChatProfile()
        {
            CreateMap<Friendship, FriendshipModel>().ReverseMap();
        }
    }
}
