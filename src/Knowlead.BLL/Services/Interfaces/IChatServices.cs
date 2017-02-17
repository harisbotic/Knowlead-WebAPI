using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.ChatModels;
using Knowlead.DTO.ChatModels;

namespace Knowlead.Services.Interfaces
{
    public interface IChatServices
    {
        Task<ChatMessage> SendChatMessage(ChatMessageModel chatMessageModel, Guid senderId);
        Task<List<ChatMessage>> GetConversation(Guid userOneId, Guid userTwoId, string fromRowKey, int numItems = 10);
        Task<List<Conversation>> GetConversations(Guid applicationUserId, DateTimeOffset fromDateTime, int numItems = 10);
    }
}
