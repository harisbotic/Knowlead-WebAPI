using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.ChatModels
{
    public class ChatMessageModel
    {
        [MyRequired]
        public string Message { get; set; }
        [MyRequired]
        public Guid? SendToId { get; set; }
        public Guid? SenderId { get; set; }
      
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
    }
}