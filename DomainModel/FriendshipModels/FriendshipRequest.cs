using System;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FriendshipModels
{
    [Table("FriendshipRequests")]
    public class FriendshipRequest
    {
        public Guid UserSentId { get; set; }
        public ApplicationUser UserSent { get; set; }

        public Guid UserReceivedId { get; set; }
        public ApplicationUser UserReceived { get; set; }
    }
}