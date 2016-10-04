using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FriendshipModels
{
    public class Friendship
    {
        [MyRequired]
        public Guid UserSentId { get; set; }
        public ApplicationUser UserSent { get; set; }

        [MyRequired]
        public Guid UserAcceptedId { get; set; }
        public ApplicationUser UserAccepted { get; set; }
    }
}