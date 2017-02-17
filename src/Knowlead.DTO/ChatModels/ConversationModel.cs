using System;

namespace Knowlead.DTO.ChatModels
{
    public class ConversationModel
    {
        public bool IsMessageSender { get; set; }
        public string LastMessage { get; set; }

        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}