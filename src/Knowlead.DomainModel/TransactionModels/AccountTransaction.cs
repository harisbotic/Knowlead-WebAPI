using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.TransactionModels
{
    public class AccountTransaction : EntityBase
    {
        [Key]
        public Guid AccountTransactionId { get; set; }

        [MyRequired]
        public int MinutesChangeAmount { get; set; }
        [MyRequired]
        public int PointsChangeAmount { get; set; }

        [MyRequired]
        public int FinalMinutesBalance { get; set; }
        [MyRequired]
        public int FinalPointsBalance { get; set; }

        [MyRequired]
        public String Reason { get; set; }

        [MyRequired]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; //TODO: Use inherited

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}