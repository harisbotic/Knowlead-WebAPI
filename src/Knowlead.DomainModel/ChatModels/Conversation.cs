using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Knowlead.DomainModel.ChatModels
{
    public class Conversation : TableEntity
    {
        public bool IsMessageSender { get; set; }
        public string LastMessage { get; set; }

        public Conversation(Guid partitionUserId, Guid rowUserId)
        {
            this.PartitionKey = partitionUserId.ToString();
            this.RowKey = rowUserId.ToString();
            this.Timestamp = DateTime.UtcNow;
        }

        public Conversation(Guid partitionUserId, Guid rowUserId, string lastMessage, Guid messageSender): this(partitionUserId, rowUserId)
        {
            LastMessage = lastMessage;
            IsMessageSender = messageSender.Equals(partitionUserId)? true : false;
        }

        public Conversation() { } // TableEntity requires parameter-less constructor
    }
}