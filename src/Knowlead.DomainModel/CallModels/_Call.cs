using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.CallModels
{
    public class _Call
    {
        [Key]
        public Guid CallId { get; set; }
        public bool Failed { get; set; } = false;
        public string FailReason { get; set; }
        public double Duration { get; set; }
        public DateTime EndDate { get; set; }
        
        [MyRequired]
        public Guid CallerId { get; set; }
        public ApplicationUser Caller { get; set; }

        public _Call()
        {
            this.EndDate = DateTime.UtcNow;
        }
    }
}