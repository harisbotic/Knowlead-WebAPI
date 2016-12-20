using System;
using System.Threading.Tasks;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IUserRelationshipRepository
    {
        Task<bool> SendFriendRequest(Guid currentUserId, Guid otherUserId);
    }
}