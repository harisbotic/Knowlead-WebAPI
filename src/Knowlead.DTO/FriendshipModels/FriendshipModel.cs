using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.FriendshipModels
{
    public class FriendshipModel
    {
        [MyRequired]
        public Guid UserSentId { get; set; }
        public ApplicationUserModel UserSent { get; set; }

        [MyRequired]
        public Guid UserAcceptedId { get; set; }
        public ApplicationUserModel UserAccepted { get; set; }
    }
}