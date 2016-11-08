using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Knowlead.Services
{
    public class BlobServices : IBlobServices
    {
        private const string IMG_CONTAINER_NAME = "images";
        private const string FILE_CONTAINER_NAME = "files";
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _config;
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _storageClient;
        private CloudBlobContainer _imageContainer;
        private CloudBlobContainer _fileContainer;

        public BlobServices(IHostingEnvironment hostingEnvironment, IConfigurationRoot config)
        {
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            
            _storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(_config["AzureStorageAccount:accName"], _config["AzureStorageAccount:key"]), false);
            _storageClient = _storageAccount.CreateCloudBlobClient();
            _imageContainer = _storageClient.GetContainerReference(IMG_CONTAINER_NAME);
            _fileContainer = _storageClient.GetContainerReference(FILE_CONTAINER_NAME);
        }

        public async Task<ImageBlobModel> SaveImageOnAzureAsync(IFormFile formFile)
        {
            ImageBlobModel imageBlobModel = Mapper.Map<ImageBlobModel>(formFile);

            await _imageContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            CloudBlockBlob blockBlob = _imageContainer.GetBlockBlobReference($"{imageBlobModel.Id}.{imageBlobModel.Extension}");

            using (Stream stream = formFile.OpenReadStream())
            {
                stream.Position = 0;
                await blockBlob.UploadFromStreamAsync(stream);
            }

            return imageBlobModel;
        }
        
        public async Task<FileBlobModel> SaveFileOnAzureAsync (IFormFile formFile)
        {
            FileBlobModel fileBlobModel = Mapper.Map<FileBlobModel>(formFile);

            await _fileContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            CloudBlockBlob blockBlob = _fileContainer.GetBlockBlobReference($"{fileBlobModel.Id}.{fileBlobModel.Extension}");

            using (Stream stream = formFile.OpenReadStream())
            {
                stream.Position = 0;
                await blockBlob.UploadFromStreamAsync(stream);
            }

            return fileBlobModel;
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