using System;
using static Knowlead.Common.Utils;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DomainModel.ChatModels
{
    public class Friendship : EntityBase
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

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Friendship(Guid currentUserId, Guid otherUserId, FriendshipStatus Status)
        {
            var bsTuple = GetBiggerSmallerGuidTuple(currentUserId, otherUserId);

            this.ApplicationUserBiggerId = bsTuple.Item1;
            this.ApplicationUserSmallerId = bsTuple.Item2;

            this.Status = Status;
            this.LastActionById = currentUserId;
        }
        public Friendship() //Just for EF
        {
        }
    }
}