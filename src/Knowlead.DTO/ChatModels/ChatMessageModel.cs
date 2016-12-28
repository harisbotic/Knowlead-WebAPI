using System;

namespace Knowlead.DomainModel.ChatModels
{
    public class ChatMessageModel
    {
        public string Message { get; set; }
        public Guid SenderId { get; set; }

      
        // public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }

    }
}