using System;
using System.Linq;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DTO.P2PModels;

namespace Knowlead.DTO.CallModels
{
    public class P2PCallModel : _CallModel
    {
        public Guid CallReceiverId { get {return this.Peers.Where(x => !x.PeerId.Equals(this.Caller.PeerId)).Select(x => x.PeerId).FirstOrDefault();} 
                                     set { this.CallReceiverId = value;} }

        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p { get; set; }

        public P2PCallModel(P2P p2p, Guid callerId, String connectionId) : base(callerId, connectionId)
        {
            this.P2pId = p2p.P2pId;
            this.Peers.Add(new PeerInfoModel(p2p.ScheduledWithId.GetValueOrDefault()));
        }
    }
}