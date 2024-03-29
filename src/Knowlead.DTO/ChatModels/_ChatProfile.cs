using AutoMapper;
using Knowlead.DomainModel.ChatModels;

namespace Knowlead.DTO.ChatModels
{
    public class _ChatProfile : Profile
    {
        public _ChatProfile()
        {
            CreateMap<Friendship, FriendshipModel>().ReverseMap();

            CreateMap<ChatMessageModel, ChatMessage>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientId))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ChatMessage, ChatMessageModel>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.UtcDateTime));
                
            CreateMap<Conversation, ConversationModel>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.UtcDateTime));

        }
    }
}
