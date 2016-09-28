using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.FriendshipModels
{
    public class FriendshipRequestModel
    {
        [Required]
        public Guid UserSentId { get; set; }
        public ApplicationUserModel UserSent { get; set; }

        [Required]
        public Guid UserReceivedId { get; set; }
        public ApplicationUserModel UserReceived { get; set; }
    }
}