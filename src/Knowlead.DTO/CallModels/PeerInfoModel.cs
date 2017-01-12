using System;
using System.Collections.Generic;
using Knowlead.DTO.UserModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DTO.CallModels
{
    public class PeerInfoModel
    {
        public Guid PeerId { get; private set; }
        public ApplicationUserModel Peer { get; private set; }

        public String ConnectionId { get; set; }
        public List<String> sdps { get; private set; } = new List<String>();

        public PeerStatus Status { get; private set; }

        public bool UpdateStatus(PeerStatus peerStatus)
        {
            bool successful = true;

            Status = peerStatus;
            
            return successful;
        }

        public bool UpdateStatus(bool accepted)
        {
            bool successful = true;

            if(accepted)
                Status = PeerStatus.Accepted;
            else
                Status = PeerStatus.Rejected;
            
            return successful;
        }

        public PeerInfoModel(Guid peerId, PeerStatus status = PeerStatus.Waiting)
        {
            this.PeerId = peerId;
            this.Status = status;
        }

        public PeerInfoModel(Guid peerId, String connectionId, PeerStatus status = PeerStatus.Waiting) : this(peerId, status)
        {
            this.ConnectionId = connectionId;
        }
    }
}