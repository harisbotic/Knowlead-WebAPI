using System;
using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DTO.CallModels
{
    public class _CallModel
    {
        [MyRequired]
        public Guid CallId { get; set; }
        public bool Failed { get; set; }
        public string FailReason { get; set; }
        [MyRequired]
        public Guid CallerId { get; set; }
        public ApplicationUser Caller { get; set; }
        public Dictionary<Guid, string> Participants { get; set; }
        public int Duration { get; set; }
        public DateTime EndDate { get; set; }

        public _CallModel()
        {
            this.CallId = Guid.NewGuid(); //DTO only
            this.Failed = false;
            this.EndDate = DateTime.UtcNow;
        }
    }
}