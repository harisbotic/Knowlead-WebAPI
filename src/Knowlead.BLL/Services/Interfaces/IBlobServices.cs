using System.Threading.Tasks;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Microsoft.AspNetCore.Http;

namespace Knowlead.Services.Interfaces
{
    public interface IBlobServices
    {

        Task<ImageBlob> SaveImageOnAzureAsync(IFormFile formFile);
        Task<FileBlob> SaveFileOnAzureAsync(IFormFile formFile);
        Task<bool> DeleteImageOnAzureAsync(ImageBlobModel imageBlobModel, ApplicationUser applicationUser);
        Task<bool> DeleteFileOnAzureAsync(FileBlobModel imageBlobModel, ApplicationUser applicationUser);
    }
}
