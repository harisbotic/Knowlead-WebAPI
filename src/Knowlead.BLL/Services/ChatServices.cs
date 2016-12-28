using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.ChatModels;
using Knowlead.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Knowlead.Services
{
    public class ChatServices : IChatServices
    {
        private readonly IConfigurationRoot _config;
        private readonly CloudTable _table;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;

        public ChatServices(IConfigurationRoot config)
        {
            _config = config;
            
            _storageAccount = new CloudStorageAccount(new StorageCredentials(_config["AzureStorageAccount:accName"], _config["AzureStorageAccount:key"]), false); //TODO: Change to true for https
            _tableClient = _storageAccount.CreateCloudTableClient();

            _table = _tableClient.GetTableReference("chat");
        }

        public async Task<TableResult> SendChatMessage(Guid senderId, Guid sendToId, String message)
        {
            await _table.CreateIfNotExistsAsync(); //Should be in constructor but can't cuz async
            
            ChatMessage entity = new ChatMessage(senderId, sendToId, message);

            TableOperation operation = TableOperation.Insert(entity);

            return await _table.ExecuteAsync(operation);
        }
        
    }
}