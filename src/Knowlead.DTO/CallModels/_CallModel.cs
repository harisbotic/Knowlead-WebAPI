using System;
using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DTO.CallModels
{
    public class _CallModel : EntityBaseModel
    {
        [MyRequired]
        public Guid CallId { get; set; }
        public bool Failed { get; set; }
        public string FailReason { get; set; }
        [MyRequired]
        public PeerInfoModel Caller { get; set; }
        public DateTime StartDate { get; private set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        public double duration { get { return -StartDate.Subtract(EndDate.HasValue?EndDate.Value:DateTime.UtcNow).TotalMinutes;} private set{}}
        public bool Sealed { get; set; }

        public bool Inviting { get; set; }
        public DateTime? InactiveSince { get; set; }

        public bool CallStarted { get; set; }
        public List<PeerInfoModel> Peers { get; set; }

        public _CallModel(Guid callerId, String connectionId)
        {
            this.CallStarted = false;
            this.Failed = false;
            this.CallId = Guid.NewGuid();
            
            this.Caller = new PeerInfoModel(callerId, connectionId, PeerStatus.Accepted);
            this.Peers = new List<PeerInfoModel>(){this.Caller};
        }
    }
}