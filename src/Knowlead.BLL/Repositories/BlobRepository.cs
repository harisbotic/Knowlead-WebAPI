using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.BlobModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private ApplicationDbContext _context;

        public BlobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateFile(FileBlob fileBlob, ApplicationUser applicationUser)
        {
            fileBlob.UploadedById = applicationUser.Id;

            _context.FileBlobs.Add(fileBlob);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<FileBlobModel>(fileBlob)
            });
        }

        public async Task<IActionResult> CreateImage(ImageBlob imageBlob, ApplicationUser applicationUser)
        {
            imageBlob.UploadedById = applicationUser.Id;

            _context.ImageBlobs.Add(imageBlob);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<ImageBlobModel>(imageBlob)
            });
        }

        public async Task<IActionResult> DeleteBlob(Guid filename, ApplicationUser applicationUser)
        {
            var blob = _context.Blobs.Where(x => x.BlobId == filename).FirstOrDefault();

            if(blob == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(_Blob));

            if(blob.UploadedById != applicationUser.Id)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.OwnershipError)));

            _context.Blobs.Remove(blob);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{});
        }

        private async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}