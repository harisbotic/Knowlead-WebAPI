using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DTO.CallModels
{
    public class FriendCallModel : _CallModel
    {
        [MyRequired]
        public Guid ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public FriendCallModel() : base()
        {
        }
    }
}