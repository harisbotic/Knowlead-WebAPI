namespace Knowlead.Common.Configurations.AppSettings
{
    public class AppSettings
    {
        public const string Path = "./Knowlead.Common/Configurations/AppSettings";
        
        public ConnectionStrings ConnectionStrings { get; set; }
        public BaseUrls BaseUrls { get; set; }
        public AzureStorageAccount AzureStorageAccount { get; set; }
    }

    public class ConnectionStrings
    {
        public string KnowleadSQL { get; set; }
    }

    public class BaseUrls
    {
        public string WebClient { get; set; }
        public string WebApi { get; set; }
        public string Auth { get; set; }
    }
        
    public class AzureStorageAccount
    {
        public string AccountName { get; set; }
        public string Key { get; set; }
    }
}