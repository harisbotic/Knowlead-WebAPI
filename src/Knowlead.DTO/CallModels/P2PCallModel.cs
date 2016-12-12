using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.P2PModels;

namespace Knowlead.DTO.CallModels
{
    public class P2PCallModel : _CallModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p { get; set; }

        public P2PCallModel(int p2pId, Guid callerId, String connectionId) : base(callerId, connectionId)
        {
            P2pId = p2pId;
        }
    }
}