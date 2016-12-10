using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.CallModels
{
    public class FriendCall : _Call
    {
        [MyRequired]
        public Guid ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public FriendCall() : base()
        {
        }
    }
}