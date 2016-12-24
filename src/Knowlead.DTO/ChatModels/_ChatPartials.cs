using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.ChatModels
{
    public class ChangeFriendshipStatusModel
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        [MyRequired]
        public FriendshipDTOActions Action { get; set; }

        public enum FriendshipDTOActions 
        {
            NewRequest = 0,
            AcceptRequest = 1,
            DeclineRequest = 2,
            CancelRequest = 3,
            RemoveFriend = 4,
            BlockUser = 5,
            UnblockUser = 6
        }
    }
}
