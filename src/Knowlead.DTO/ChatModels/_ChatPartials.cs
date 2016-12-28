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

    public class NewChatMessage
    {
        [MyRequired]
        public string Message { get; set; }

        [MyRequired]
        public Guid SendToId { get; set; }
    }
}
