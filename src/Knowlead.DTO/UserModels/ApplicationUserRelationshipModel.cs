using System;
using Knowlead.Common.DataAnnotations;

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
        public UserRelationshipStatus Status { get; set; }

        [MyRequired]
        public Guid LastActionById { get; set; }
        public ApplicationUserModel LastActionBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public enum UserRelationshipStatus 
        {
            Pending = 0,
            Accepted = 1,
            Declined = 2,
            Blocked = 3
        }
    }
}