using System;
using Microsoft.WindowsAzure.Storage.Table;
using static Knowlead.Common.Utils;

namespace Knowlead.DomainModel.ChatModels
{
    public class ChatMessage : TableEntity
    {
        public string Message { get; set; }
        public Guid SenderId { get; set; }

        public ChatMessage(Guid senderId, Guid sendToId)
        {
            this.PartitionKey = GenerateChatMessagePartitionKey(senderId, sendToId);
            this.RowKey = DateTime.UtcNow.Ticks.ToString();
            this.Timestamp = DateTime.UtcNow;

            SenderId = senderId;
        }

        public ChatMessage(Guid senderId, Guid sendToId, string message): this(senderId, sendToId)
        {
            Message = message;
        }

        public ChatMessage() { } // TableEntity requires parameter-less constructor
    }
}