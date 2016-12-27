// using System;
// using System.IO;
// using System.Threading.Tasks;
// using AutoMapper;
// using Knowlead.DomainModel.BlobModels;
// using Knowlead.DomainModel.UserModels;
// using Knowlead.DTO.BlobModels;
// using Knowlead.Services.Interfaces;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.WindowsAzure.Storage;
// using Microsoft.WindowsAzure.Storage.Blob;
// using Microsoft.WindowsAzure.Storage.Table;

namespace Knowlead.Services
{
    public class ChatServices
    {
        // private readonly IConfigurationRoot _config;
        // private readonly CloudTable _table;
        // private readonly CloudStorageAccount _storageAccount;
        // private readonly CloudTableClient _tableClient;

        // public ChatServices(IConfigurationRoot config)
        // {
        //     _config = config;
            
        //     _storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_config["AzureStorageAccount:accName"], _config["AzureStorageAccount:key"]), false);
        //     _tableClient = _storageAccount.CreateCloudTableClient();

        //     _table = _tableClient.GetTableReference("chat_messages");
        //     _table.CreateIfNotExistsAsync();
        // }

        // public async Task<ImageBlob> SaveImageOnAzureAsync(TableEntity entity)
        // {
        //     TableOperation operation = TableOperation.InsertOrReplace(entity);

        //     TableResult retrievedResult = await _table.ExecuteAsync(operation);
        //     var e = retrievedResult.Result;

        //     return imageBlob;
        // }
        
        // public async Task<FileBlob> SaveFileOnAzureAsync (IFormFile formFile)
        // {
        //     FileBlob fileBlob = Mapper.Map<FileBlob>(formFile);

        //     await _fileContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

        //     CloudBlockBlob blockBlob = _fileContainer.GetBlockBlobReference($"{fileBlob.BlobId}.{fileBlob.Extension}");

        //     using (Stream stream = formFile.OpenReadStream())
        //     {
        //         stream.Position = 0;
        //         await blockBlob.UploadFromStreamAsync(stream);
        //     }

        //     return fileBlob;
        // }

        // public async Task<bool> DeleteImageOnAzureAsync(ImageBlobModel imageBlobModel, ApplicationUser applicationUser)
        // { 
        //     CloudBlockBlob blockBlob = _imageContainer.GetBlockBlobReference(imageBlobModel.Filename.ToString());

        //     return await blockBlob.DeleteIfExistsAsync();
        // }
        
        // public async Task<bool> DeleteFileOnAzureAsync(FileBlobModel fileBlobModel, ApplicationUser applicationUser)
        // { 
        //     CloudBlockBlob blockBlob = _fileContainer.GetBlockBlobReference(fileBlobModel.Filename.ToString());

        //     return await blockBlob.DeleteIfExistsAsync();
        // }

        // private String GetFileExtension(string filename)
        // {
        //     return "";
        // }
    }
}