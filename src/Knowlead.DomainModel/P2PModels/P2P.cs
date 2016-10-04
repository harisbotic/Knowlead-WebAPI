using System;
using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Knowlead.DomainModel.P2PModels
{
    [Table("P2Ps")]
    public class P2P
    {
        [Key]
        public int P2pId { get; set; }
        
        [MyRequired]
        public float Rate { get; set; }
        public DateTime StartingAt { get; set; }
        
        [MyRequired]
        public P2PStatus Status { get; set; }
        public enum P2PStatus 
        {
            Inactive, PendingAction, Scheduled, Finsihed
        }

        public P2P()
        {
            this.Status = P2PStatus.Inactive;
        }
    }
}