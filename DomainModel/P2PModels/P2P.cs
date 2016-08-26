using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowlead.DomainModel.P2PModels
{
    [Table("P2Ps")]
    public class P2P
    {
        public int P2pId { get; set; }
        [Required]
        public float Rate { get; set; }
        public DateTime StartingAt { get; set; }
        
        [Required]
        public UserStatus Status { get; set; }
        public enum UserStatus 
        {
            Unactive, PendingAction, Scheduled, Finsihed
        }

        public P2P()
        {
            this.Status = UserStatus.Unactive;
        }
    }
}