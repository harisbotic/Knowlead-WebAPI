using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.ChatModels
{
    public class ChatMessageModel
    {
        [MyRequired]
        public string Message { get; set; }
        [MyRequired]
        public Guid? RecipientId { get; set; }
        public Guid? SenderId { get; set; }
      
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}