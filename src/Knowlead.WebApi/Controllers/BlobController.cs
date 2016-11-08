using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.WebApi.Attributes;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Knowlead.Services.Interfaces;
using Knowlead.BLL.Repositories.Interfaces;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class BlobController : Controller
    {
        private readonly static string[] ImageFileExtensions = {".jpg", ".jpeg", ".gif", ".png"};
        private readonly IBlobServices _blobServices;
        private readonly IBlobRepository _blobRepository;
        private readonly Auth _auth;
        public BlobController(IBlobServices blobServices,
                              IBlobRepository blobRepository,
                             Auth auth)
        {
            _blobServices = blobServices;
            _blobRepository = blobRepository;
            _auth = auth;
        }

        [HttpPost("upload"), ReallyAuthorize, ValidateModel]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var applicationUser = await _auth.GetUser();
            
            var filename = file.FileName;
            var isImage = ImageFileExtensions.Any(ex => filename.EndsWith(ex, StringComparison.CurrentCultureIgnoreCase));

            if(isImage)
            {
                var i = await _blobServices.SaveImageOnAzureAsync(file);
                return await _blobRepository.CreateImage(i, applicationUser);

            }
            else
            {
                var i = await _blobServices.SaveFileOnAzureAsync(file);
                return await _blobRepository.CreateFile(i, applicationUser);
            }
        }
    }
}