using System;
using Knowlead.Common.DataAnnotations;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserRelationship
    {

        [MyRequired]
        public Guid ApplicationUserBiggerId { get; set; }
        public ApplicationUser ApplicationUserBigger { get; set; }

        [MyRequired] //GuidBiggerThen() attribute or through model validation
        public Guid ApplicationUserSmallerId { get; set; }
        public ApplicationUser ApplicationUserSmaller { get; set; }

        [MyRequired]
        public FriendshipStatus Status { get; set; }

        [MyRequired]
        public Guid LastActionById { get; set; }
        public ApplicationUser LastActionBy { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUserRelationship(Guid currentUserId, Guid otherUserId, FriendshipStatus Status)
        {
            if(currentUserId.Equals(otherUserId))
                throw new Exception(); // TODO: Should be ErrorModelException

            var biggerGuid = (currentUserId.CompareTo(otherUserId) > 0)? currentUserId : otherUserId;
            var smallerGuid = (currentUserId.CompareTo(otherUserId) < 0)? currentUserId : otherUserId;

            this.ApplicationUserBiggerId = biggerGuid;
            this.ApplicationUserSmallerId = smallerGuid;

            this.Status = Status;
            this.LastActionById = currentUserId;
        }
        public ApplicationUserRelationship()
        { //Just for EF
        }
    }
}