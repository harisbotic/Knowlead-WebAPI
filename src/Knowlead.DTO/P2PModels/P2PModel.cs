using System;
using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.P2PModels
{
    public class P2PModel
    {        
        [MyRequired]
        public string Title { get; set; }
        
        [MyRequired]
        public float ChargePerMinute { get; set; }
        
        [MyRequired]
        public DateTime Deadline { get; set; }
        
        public DateTime ScheduledAt { get; set; }

        public List<LanguageModel> Languages { get; set; }
        
        [MyRequired]
        public int FosId { get; set; }
        public FOSModel Fos { get; set; }

        [MyRequired]
        public P2PStatus Status { get; set; }
        public enum P2PStatus 
        {
            Inactive, Active, Scheduled, Finsihed
        }

        public P2PModel()
        {
            this.Status = P2PStatus.Inactive;
            this.Languages = new List<LanguageModel>();
        }
    }
}