using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowlead.DomainModel.P2PModels
{
    [Table("P2Ps")]
    public class P2P
    {
        [Key]
        public int P2pId { get; set; }
        
        [Required]
        public float Rate { get; set; }
        public DateTime StartingAt { get; set; }
        
        [Required]
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