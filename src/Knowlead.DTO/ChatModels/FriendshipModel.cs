using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DTO.ChatModels
{
    public class FriendshipModel
    {
        [MyRequired]
        public Guid ApplicationUserBiggerId { get; set; }
        public ApplicationUser ApplicationUserBigger { get; set; }

        [MyRequired]
        public Guid ApplicationUserSmallerId { get; set; }
        public ApplicationUser ApplicationUserSmaller { get; set; }

        [MyRequired]
        public FriendshipStatus Status { get; set; }

        [MyRequired]
        public Guid LastActionById { get; set; }
        public ApplicationUser LastActionBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}