using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.WebApi.Attributes;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Knowlead.Services.Interfaces;
using Knowlead.DTO.ResponseModels;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class BlobController : Controller
    {
        private readonly static string[] ImageFileExtensions = {".jpg", ".jpeg", ".gif", ".png"};
        private readonly IBlobServices _blobServices;
        private readonly Auth _auth;
        public BlobController(IBlobServices blobServices,
                             Auth auth)
        {
            _blobServices = blobServices;
            _auth = auth;
        }

        [HttpPost("upload"), ReallyAuthorize, ValidateModel]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var applicationUser = await _auth.GetUser();
            
            var filename = file.FileName;
            var isImage = ImageFileExtensions.Any(ex => filename.EndsWith(ex, StringComparison.CurrentCultureIgnoreCase));

            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    if(isImage)
                        _blobServices.SaveImageToLocalStorage(filename, fileContent);
                    else
                        _blobServices.SaveFileToLocalStorage(filename, fileContent);
                }
            }

            return Ok(new ResponseModel());
        }
    }
}