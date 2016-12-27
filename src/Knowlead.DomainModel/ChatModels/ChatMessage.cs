using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Knowlead.DomainModel.ChatModels
{
    public class ChatMessage : TableEntity
    {
        public string Message { get; private set; }
        public Guid SenderId { get; set; }

        public ChatMessage(Guid userOneId, string userTwoId, string message)
        {
            PartitionKey = $"{userOneId} {userTwoId}";
            RowKey = Guid.NewGuid().ToString();

            Message = message;
            Timestamp = DateTime.UtcNow;
        }
    }
}