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
        public UserStatus Status { get; set; }
        public enum UserStatus 
        {
            Inactive, PendingAction, Scheduled, Finsihed
        }

        public P2PModel()
        {
            this.Status = UserStatus.Inactive;
        }
    }
}