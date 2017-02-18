using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.CallModels
{
    public class FriendCall : _Call
    {
        [MyRequired]
        public Guid CallReceiverId { get; set; }
        public ApplicationUser CallReceiver { get; set; }

        public FriendCall() : base()
        {
        }
    }
}