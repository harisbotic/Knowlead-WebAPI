using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.Common.Configurations.AppSettings;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Knowlead.Services
{
    public class BlobServices : IBlobServices
    {
        public const string IMG_CONTAINER_NAME = "images";
        public const string FILE_CONTAINER_NAME = "files";

        private readonly AppSettings _appSettings;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _storageClient;
        private readonly CloudBlobContainer _imageContainer;
        private readonly CloudBlobContainer _fileContainer;

        public BlobServices(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            
            _storageAccount = new CloudStorageAccount(new StorageCredentials(_appSettings.AzureStorageAccount.AccountName, _appSettings.AzureStorageAccount.Key), true);
            _storageClient = _storageAccount.CreateCloudBlobClient();
            _imageContainer = _storageClient.GetContainerReference(IMG_CONTAINER_NAME);
            _fileContainer = _storageClient.GetContainerReference(FILE_CONTAINER_NAME);
        }

        public async Task<ImageBlob> SaveImageOnAzureAsync(IFormFile formFile)
        {
            ImageBlob imageBlob = Mapper.Map<ImageBlob>(formFile);

            CloudBlockBlob blockBlob = _imageContainer.GetBlockBlobReference($"{imageBlob.BlobId}");
            blockBlob.Properties.ContentType = formFile.ContentType;
            blockBlob.Properties.ContentDisposition = string.Format("attachment;filename=\"{0}\"", formFile.FileName);

            using (Stream stream = formFile.OpenReadStream())
            {
                stream.Position = 0;
                await blockBlob.UploadFromStreamAsync(stream);
            }

            return imageBlob;
        }
        
        public async Task<FileBlob> SaveFileOnAzureAsync (IFormFile formFile)
        {
            FileBlob fileBlob = Mapper.Map<FileBlob>(formFile);

            CloudBlockBlob blockBlob = _fileContainer.GetBlockBlobReference($"{fileBlob.BlobId}");
            blockBlob.Properties.ContentType = formFile.ContentType;

            using (Stream stream = formFile.OpenReadStream())
            {
                stream.Position = 0;
                await blockBlob.UploadFromStreamAsync(stream);
            }

            return fileBlob;
        }

        public async Task<bool> DeleteImageOnAzureAsync(ImageBlobModel imageBlobModel, ApplicationUser applicationUser)
        { 
            CloudBlockBlob blockBlob = _imageContainer.GetBlockBlobReference(imageBlobModel.Filename.ToString());

            return await blockBlob.DeleteIfExistsAsync();
        }
        
        public async Task<bool> DeleteFileOnAzureAsync(FileBlobModel fileBlobModel, ApplicationUser applicationUser)
        { 
            CloudBlockBlob blockBlob = _fileContainer.GetBlockBlobReference(fileBlobModel.Filename.ToString());

            return await blockBlob.DeleteIfExistsAsync();
        }

        private String GetFileExtension(string filename)
        {
            return "";
        }
    }
}