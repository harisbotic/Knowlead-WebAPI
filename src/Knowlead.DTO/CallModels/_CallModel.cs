using System;
using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;
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
        public Guid CallerId { get; set; }
        public ApplicationUserModel Caller { get; set; }
        public int Duration { get; set; }
        public DateTime? EndDate { get; set; }


        public bool CallStarted { get; set; }
        public List<PeerInfoModel> Peers { get; set; }

        public _CallModel(Guid callerId, String connectionId)
        {
            this.Failed = false;
            this.CallId = Guid.NewGuid();
            this.CallerId = callerId;
            this.Peers = new List<PeerInfoModel>(){new PeerInfoModel(callerId, connectionId, PeerStatus.Accepted)};
            this.CallStarted = false;
        }
    }
}