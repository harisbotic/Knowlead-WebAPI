using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FriendshipModels
{
    [Table("FriendshipRequests")]
    public class FriendshipRequest
    {
        [Required]
        public Guid UserSentId { get; set; }
        public ApplicationUser UserSent { get; set; }

        [Required]
        public Guid UserReceivedId { get; set; }
        public ApplicationUser UserReceived { get; set; }
    }
}