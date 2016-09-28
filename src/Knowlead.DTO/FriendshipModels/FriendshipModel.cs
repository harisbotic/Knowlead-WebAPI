using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.FriendshipModels
{
    public class FriendshipModel
    {
        [Required]
        public Guid UserSentId { get; set; }
        public ApplicationUserModel UserSent { get; set; }

        [Required]
        public Guid UserAcceptedId { get; set; }
        public ApplicationUserModel UserAccepted { get; set; }
    }
}