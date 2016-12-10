using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.P2PModels;

namespace Knowlead.DTO.CallModels
{
    public class P2PCallModel : _CallModel
    {
        [MyRequired]
        public int P2PId { get; set; }
        public P2PModel P2P { get; set; }
        [MyRequired]
        public Guid ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public P2PCallModel() : base()
        {
        }
    }
}