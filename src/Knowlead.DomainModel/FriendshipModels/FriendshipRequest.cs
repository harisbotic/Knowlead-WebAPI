using System;
using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FriendshipModels
{
    [Table("FriendshipRequests")]
    public class FriendshipRequest
    {
        [MyRequired]
        public Guid UserSentId { get; set; }
        public ApplicationUser UserSent { get; set; }

        [MyRequired]
        public Guid UserReceivedId { get; set; }
        public ApplicationUser UserReceived { get; set; }
    }
}