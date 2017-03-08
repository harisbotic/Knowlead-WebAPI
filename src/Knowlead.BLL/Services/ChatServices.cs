using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.ChatModels;
using Knowlead.DTO.ChatModels;
using Knowlead.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using static Knowlead.Common.Constants;
using static Knowlead.Common.Constants.EnumStatuses;
using static Knowlead.Common.Utils;

namespace Knowlead.Services
{
    public class ChatServices : IChatServices
    {
        private readonly IConfigurationRoot _config;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly CloudTable _chatMsgTable;
        private readonly CloudTable _conversationTable;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;

        public ChatServices(IConfigurationRoot config, IFriendshipRepository friendshipRepository)
        {
            _config = config;
            _friendshipRepository = friendshipRepository;

            _storageAccount = new CloudStorageAccount(new StorageCredentials(_config["AzureStorageAccount:accName"], _config["AzureStorageAccount:key"]), true); //TODO: Change to true for https
            _tableClient = _storageAccount.CreateCloudTableClient();

            _chatMsgTable = _tableClient.GetTableReference("chatMessages");
            _conversationTable = _tableClient.GetTableReference("conversations");
        }

        public async Task<ChatMessage> SendChatMessage(ChatMessageModel chatMessageModel, Guid senderId)
        {
            var sendToId = chatMessageModel.SendToId.GetValueOrDefault();
            var message = chatMessageModel.Message;
            
            var friendship = await _friendshipRepository.GetFriendship(senderId, sendToId);

            if(friendship == null || friendship.Status != FriendshipStatus.Accepted)
                throw new ErrorModelException(ErrorCodes.NotInFriendlist);

            if(String.IsNullOrEmpty(message))
                throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(message));

            ChatMessage chatMessageEntity = new ChatMessage(senderId, sendToId, message);

            Conversation conversationEntity = new Conversation(senderId, sendToId, message, senderId);
            Conversation conversationEntity2 = new Conversation(sendToId, senderId, message, senderId);

            TableOperation operation = TableOperation.Insert(chatMessageEntity);
            TableOperation operation2 = TableOperation.InsertOrReplace(conversationEntity);
            TableOperation operation3 = TableOperation.InsertOrReplace(conversationEntity2); 
            
            await _chatMsgTable.ExecuteAsync(operation);
            _conversationTable.ExecuteAsync(operation2).ConfigureAwait(true);
            _conversationTable.ExecuteAsync(operation3).ConfigureAwait(true);

            return chatMessageEntity;
        }

        public async Task<List<ChatMessage>> GetConversation(Guid userOneId, Guid userTwoId, string fromRowKey, int numItems = 10)
        {   
            var partitionKey = GenerateChatMessagePartitionKey(userOneId, userTwoId);

            TableQuery<ChatMessage> rangeQuery = new TableQuery<ChatMessage>().Where(
            TableQuery.CombineFilters(
            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
            TableOperators.And,
            TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, fromRowKey))).Take(numItems);

            var tableQuery =  await _chatMsgTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            return tableQuery.Results;
        }

        public async Task<List<Conversation>> GetConversations(Guid applicationUserId, DateTimeOffset fromDateTime, int numItems = 10)
        {   
            TableQuery<Conversation> rangeQuery = new TableQuery<Conversation>().Where(
            TableQuery.CombineFilters(
            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, applicationUserId.ToString()),
            TableOperators.And,
            TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThan, fromDateTime))).Take(numItems);

            var tableQuery =  await _conversationTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            return tableQuery.Results;
        }
    }
}