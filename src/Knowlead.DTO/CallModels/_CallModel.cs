using System;
using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DTO.CallModels
{
    public class _CallModel
    {
        [MyRequired]
        public Guid CallId { get; set; }
        public bool Failed { get; set; }
        public string FailReason { get; set; }
        [MyRequired]
        public PeerInfoModel Caller { get; set; }
        public int Duration { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Sealed { get; set; }


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