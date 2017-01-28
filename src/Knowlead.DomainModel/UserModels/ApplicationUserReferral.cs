using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserReferral
    {
        [MyRequired]
        public Guid NewRegistredUserId { get; set; }
        public ApplicationUser NewRegistredUser { get; set; }

        [MyRequired]
        public Guid ReferralUserId { get; set; }
        public ApplicationUser ReferralUser { get; set; }

        public bool UserRegistred { get; set; } = false;

        public ApplicationUserReferral(Guid newRegistredUserId, Guid referralUserId)
        {
            this.NewRegistredUserId = newRegistredUserId;
            this.ReferralUserId = referralUserId;
        }

        public ApplicationUserReferral() //Just for EF
        {

        }
    }
}