using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Microsoft.AspNetCore.Http;

namespace Knowlead.Services.Interfaces
{
    public interface IBlobServices
    {

        Task<ImageBlobModel> SaveImageOnAzureAsync(IFormFile formFile);
        Task<FileBlobModel> SaveFileOnAzureAsync(IFormFile formFile);
        Task<bool> DeleteImageOnAzureAsync(ImageBlobModel imageBlobModel, ApplicationUser applicationUser);
        Task<bool> DeleteFileOnAzureAsync(FileBlobModel imageBlobModel, ApplicationUser applicationUser);
    }
}
