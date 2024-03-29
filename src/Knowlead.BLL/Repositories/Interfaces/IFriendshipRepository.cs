using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.ChatModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<Friendship> GetFriendship(Guid userIdOne, Guid userIdTwo);
        Task<List<Friendship>> GetAllFriends (Guid applicationUserId, FriendshipStatus? status);
        Task<Friendship> SendFriendRequest (Guid currentUserId, Guid otherUserId);
        Task<Friendship> RespondToFriendRequest (Guid currentUserId, Guid otherUserId, bool isAccepting);
        Task<Friendship> CancelFriendRequest (Guid currentUserId, Guid otherUserId);
        Task<Friendship> RemoveFriend (Guid currentUserId, Guid otherUserId);
        Task<Friendship> BlockUser (Guid currentUserId, Guid otherUserId);
        Task<Friendship> UnblockUser (Guid currentUserId, Guid otherUserId);
    }
}