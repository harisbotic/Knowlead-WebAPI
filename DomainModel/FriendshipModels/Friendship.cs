using System;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FriendshipModels
{
    public class Friendship
    {
        public Guid UserSentId { get; set; }
        public ApplicationUser UserSent { get; set; }

        public Guid UserAcceptedId { get; set; }
        public ApplicationUser UserAccepted { get; set; }
    }
}