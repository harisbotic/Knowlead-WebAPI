using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IUserRelationshipRepository
    {
        Task<List<ApplicationUserRelationship>> GetAllFriends (Guid applicationUserId);
        Task<ApplicationUserRelationship> SendFriendRequest (Guid currentUserId, Guid otherUserId);
        Task<ApplicationUserRelationship> RespondToFriendRequest (Guid currentUserId, Guid otherUserId, bool isAccepting);
        Task<ApplicationUserRelationship> CancelFriendRequest (Guid currentUserId, Guid otherUserId);
        Task<ApplicationUserRelationship> RemoveFriend (Guid currentUserId, Guid otherUserId);
        Task<ApplicationUserRelationship> BlockUser (Guid currentUserId, Guid otherUserId);
        Task<ApplicationUserRelationship> UnblockUser (Guid currentUserId, Guid otherUserId);
    }
}