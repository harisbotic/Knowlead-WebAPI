using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO
{
    public class PromoCodeModel : EntityBaseModel
    {
        [MyRequired]
        public String Code { get; set; }

        [MyRequired]
        public DateTime ExpirationDate { get; set; }
        public DateTime? ActivatedAt { get; set; }
    

        [MyRequired]
        public int RewardId { get; set; }
        public RewardModel Reward { get; set; }

        public Guid? ActivatorId { get; set; }
        public ApplicationUserModel Activator { get; set; }
    }
}