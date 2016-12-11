using System;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.CallModels
{
    public class PeerInfoModel
    {
        public Guid PeerId { get; private set; }
        public ApplicationUserModel Peer { get; private set; }

        public String ConnectionId { get; private set; }
        public String SDP { get; private set; }

        public PeerStatus Status { get; private set; }

        public enum PeerStatus 
        {
            Accepted, Rejected, Waiting
        }

        public bool UpdateInfo(String connectionId = null, String sdp = null)
        {
            bool changes = false;

            if(connectionId != null)
            {
                ConnectionId = connectionId;
                changes = true;
            }
            
            if(sdp != null)
            {
                SDP = sdp;
                changes = true;
            }
            
            return changes;
        }

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