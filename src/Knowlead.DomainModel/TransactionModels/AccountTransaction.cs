using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.TransactionModels
{
    public class AccountTransaction
    {
        [Key]
        public Guid AccountTransactionId { get; set; }

        [MyRequired]
        public int MinuteChangeAmount { get; set; }
        [MyRequired]
        public int PointChangeAmount { get; set; }

        [MyRequired]
        public int FinalMinuteBalance { get; set; }
        [MyRequired]
        public int FinalPointBalance { get; set; }

        [MyRequired]
        public String Reason { get; set; }

        [MyRequired]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}