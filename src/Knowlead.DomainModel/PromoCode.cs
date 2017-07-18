using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel
{
    public class PromoCode : EntityBase
    {
        [Key]
        public int PromoCodeId { get; set; }

        [MyRequired]
        public String Code { get; set; }

        [MyRequired]
        public DateTime ExpirationDate { get; set; }
        public DateTime? ActivatedAt { get; set; }
    

        [MyRequired]
        public int RewardId { get; set; }
        public Reward Reward { get; set; }

        public Guid? ActivatorId { get; set; }
        public ApplicationUser Activator { get; set; }
    }
}