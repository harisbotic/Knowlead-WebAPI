using System;
using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.P2PModels
{
    public class P2PModel
    {        
        [Required]
        public float Rate { get; set; }
        public DateTime StartingAt { get; set; }
        
        [Required]
        public P2PStatus Status { get; set; }
        public enum P2PStatus 
        {
            Inactive, PendingAction, Scheduled, Finsihed
        }

        public P2PModel()
        {
            this.Status = P2PStatus.Inactive;
        }
    }
}