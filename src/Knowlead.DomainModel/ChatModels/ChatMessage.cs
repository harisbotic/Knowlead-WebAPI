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
            var bsTuple = GetBiggerSmallerGuidTuple(senderId, sendToId);
            this.PartitionKey = $"{bsTuple.Item1}{bsTuple.Item2}";
            this.RowKey = Guid.NewGuid().ToString();

            SenderId = senderId;
        }

        public ChatMessage(Guid senderId, Guid sendToId, string message): this(senderId, sendToId)
        {
            Message = message;
        }

        public ChatMessage() { } // TableEntity requires parameter-less constructor
    }
}