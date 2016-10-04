using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.P2PModels
{
    public class P2PModel
    {        
        [MyRequired]
        public float Rate { get; set; }
        public DateTime StartingAt { get; set; }
        
        [MyRequired]
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