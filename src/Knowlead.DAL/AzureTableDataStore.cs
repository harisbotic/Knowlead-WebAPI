namespace Knowlead.DAL
{
    public class AzureTableDataStore
    {
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