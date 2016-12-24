using System;
using Knowlead.Common.DataAnnotations;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserRelationshipModel
    {
        [MyRequired]
        public Guid ApplicationUserBiggerId { get; set; }
        public ApplicationUserModel ApplicationUserBigger { get; set; }

        [MyRequired]
        public Guid ApplicationUserSmallerId { get; set; }
        public ApplicationUserModel ApplicationUserSmaller { get; set; }

        [MyRequired]
        public FriendshipStatus Status { get; set; }

        [MyRequired]
        public Guid LastActionById { get; set; }
        public ApplicationUserModel LastActionBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}