using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.CallModels
{
    public class FriendCallModel : _CallModel
    {
        [MyRequired]
        public Guid ReceiverId { get; set; }
        public ApplicationUserModel Receiver { get; set; }

        public FriendCallModel(Guid callerId, String connectionId) : base(callerId, connectionId)
        {
        }
    }
}