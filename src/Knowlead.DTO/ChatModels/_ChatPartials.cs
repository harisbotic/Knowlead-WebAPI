using System;
using Knowlead.Common.DataAnnotations;
using static Knowlead.Common.Constants.EnumActions;

namespace Knowlead.DTO.ChatModels
{
    public class ChangeFriendshipStatusModel
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        [MyRequired]
        public FriendshipDTOActions Action { get; set; }
    }
}
