using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IBlobRepository
    {
        Task<IActionResult> CreateImage (ImageBlob imageBlobModel, ApplicationUser applicationUser);

        Task<IActionResult> CreateFile (FileBlob fileBlobModel, ApplicationUser applicationUser);

        Task<IActionResult> DeleteBlob (Guid filename, ApplicationUser applicationUser);
    }
}