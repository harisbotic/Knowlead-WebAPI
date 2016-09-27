using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FriendshipModels
{
    public class Friendship
    {
        [Required]
        public Guid UserSentId { get; set; }
        public ApplicationUser UserSent { get; set; }

        [Required]
        public Guid UserAcceptedId { get; set; }
        public ApplicationUser UserAccepted { get; set; }
    }
}