using System;
using Microsoft.WindowsAzure.Storage.Table;
using static Knowlead.Common.Utils;

namespace Knowlead.DomainModel.ChatModels
{
    public class ChatMessage : TableEntity
    {
        public string Message { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }

        public ChatMessage(Guid senderId, Guid recipientId)
        {
            this.PartitionKey = GenerateChatMessagePartitionKey(senderId, recipientId);
            this.RowKey = (DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks).ToString();

            SenderId = senderId;
            RecipientId = recipientId;
        }

        public ChatMessage(Guid senderId, Guid recipientId, string message): this(senderId, recipientId)
        {
            Message = message;
        }

        public ChatMessage() { } // TableEntity requires parameter-less constructor
    }
}