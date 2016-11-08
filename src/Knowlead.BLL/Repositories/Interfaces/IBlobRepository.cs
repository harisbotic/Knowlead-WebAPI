using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IBlobRepository
    {
        Task<IActionResult> CreateImage (ImageBlobModel imageBlobModel, ApplicationUser applicationUser);

        Task<IActionResult> CreateFile (FileBlobModel fileBlobModel, ApplicationUser applicationUser);
    }
}