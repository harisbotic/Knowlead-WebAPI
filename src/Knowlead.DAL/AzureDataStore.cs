using Knowlead.Common.Configurations.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace Knowlead.DAL
{
    public class AzureDataStore
    {
        public const string IMG_CONTAINER_NAME = "images";
        public const string FILE_CONTAINER_NAME = "files";
        
        private readonly AppSettings _appSettings;
        private readonly CloudStorageAccount _storageAccount;

        //NOSQL - Tables
        private readonly CloudTable _chatMsgTable;
        private readonly CloudTable _conversationTable;
        private readonly CloudTableClient _tableClient;

        //Blobs
        private CloudBlobClient _storageClient;
        private CloudBlobContainer _imageContainer;
        private CloudBlobContainer _fileContainer;

        public AzureDataStore(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _storageAccount = new CloudStorageAccount(new StorageCredentials(_appSettings.AzureStorageAccount.AccountName, _appSettings.AzureStorageAccount.Key), true); //TODO: Change to true for https

            _tableClient = _storageAccount.CreateCloudTableClient();
            _chatMsgTable = _tableClient.GetTableReference("chatMessages");
            _conversationTable = _tableClient.GetTableReference("conversations");

            _storageClient = _storageAccount.CreateCloudBlobClient();
            _imageContainer = _storageClient.GetContainerReference(IMG_CONTAINER_NAME);
            _fileContainer = _storageClient.GetContainerReference(FILE_CONTAINER_NAME);
        }

        public void Init()
        {
            _chatMsgTable.CreateIfNotExistsAsync();
            _conversationTable.CreateIfNotExistsAsync();

            _imageContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
            _fileContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
        }
        // private readonly CloudTable _table;  **TODO**

        // private static readonly CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

        // private static readonly CloudTableClient _tableClient = _storageAccount.CreateCloudTableClient();

        // public TableDataStore(string tableName)
        // {
        //     _table = _tableClient.GetTableReference(tableName);
        //     _table.CreateIfNotExists();
        // }

        // public TableEntity CreateOrUpdate(TableEntity entity)
        // {
        //     TableOperation operation = TableOperation.InsertOrReplace(entity);

        //     return _table.Execute(operation).Result as TableEntity;
        // }

        // public TableEntity Retrieve(TableEntity entity)
        // {
        //     TableOperation operation = TableOperation.Retrieve<TableEntity>(entity.PartitionKey, entity.RowKey);

        //     return _table.Execute(operation).Result as TableEntity;
        // }

        // public IEnumerable<T> RetrieveAll<T>(TableQuery<T> query) where T : ITableEntity, new()
        // {
        //     return _table.ExecuteQuery(query);
        // }
    }
}