using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common;
using Knowlead.DAL;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private ApplicationDbContext _context;

        public BlobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateFile(FileBlobModel fileBlobModel, ApplicationUser applicationUser)
        {
            FileBlob fileBlob = Mapper.Map<FileBlob>(fileBlobModel);
            fileBlob.UploadedById = applicationUser.Id;

            _context.Add(fileBlob);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = fileBlob
            });
        }

        public async Task<IActionResult> CreateImage(ImageBlobModel imageBlobModel, ApplicationUser applicationUser)
        {
            ImageBlob imageBlob = Mapper.Map<ImageBlob>(imageBlobModel);
            imageBlob.UploadedById = applicationUser.Id;

            _context.Add(imageBlob);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = imageBlob
            });
        }

        private async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}