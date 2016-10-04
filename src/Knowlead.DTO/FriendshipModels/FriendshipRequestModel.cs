using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.FriendshipModels
{
    public class FriendshipRequestModel
    {
        [MyRequired]
        public Guid UserSentId { get; set; }
        public ApplicationUserModel UserSent { get; set; }

        [MyRequired]
        public Guid UserReceivedId { get; set; }
        public ApplicationUserModel UserReceived { get; set; }
    }
}