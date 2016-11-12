using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common;
using Knowlead.DAL;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Knowlead.BLL.Repositories
{
    public class P2PRepository : IP2PRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public P2PRepository(ApplicationDbContext context,
                             IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public P2PModel Get(int p2pId)
        {
            var p2p = _context.P2p.Where(x => x.P2pId == p2pId)
                                  .Include(x => x.P2PFiles)
                                    .ThenInclude(x => x.FileBlob)
                                  .Include(x => x.P2PImages)
                                    .ThenInclude(x => x.ImageBlob)
                                  .FirstOrDefault();
            
            if(p2p == null && !p2p.IsDeleted)
                    return null;

            return Mapper.Map<P2PModel>(p2p);
        }

        public async Task<IActionResult> Create(P2PModel p2pModel, ApplicationUser applicationUser)
        {
            var p2p = Mapper.Map<P2P>(p2pModel);
            p2p.CreatedById = applicationUser.Id;
            
            if(p2p.Deadline != null && p2p.Deadline < DateTime.Now.AddMinutes(1))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(P2P.Deadline), Constants.ErrorCodes.IncorrectValue)));
            
            var blobIds = p2pModel.Blobs.Select(x => x.BlobId).ToList();
            var blobs = _context.Blobs.Where(x => blobIds.Contains(x.BlobId)).ToList();

            foreach (var blob in blobs)
            {
                if(blob.UploadedById != applicationUser.Id)
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(P2P.Deadline), Constants.ErrorCodes.OwnershipProblem)));
            
                if(blob.BlobType == "Image")
                    p2p.P2PImages.Add(new P2PImage{
                        P2p = p2p,
                        ImageBlobId = blob.BlobId
                    });

                else if(blob.BlobType == "File")
                    p2p.P2PFiles.Add(new P2PFile{
                    P2p = p2p,
                    FileBlobId = blob.BlobId
                });
            }
            
            _context.P2p.Add(p2p);

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }

        public async Task<IActionResult> Delete(int p2pInt, ApplicationUser applicationUser)
        {
            var p2p = _context.P2p.Where(x => x.P2pId == p2pInt).FirstOrDefault();

            if (p2p == null)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.EntityNotFound, nameof(P2P))));

            if(p2p.CreatedById != applicationUser.Id)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.OwnershipProblem)));

            if(p2p.IsDeleted)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.AlreadyDeleted, nameof(P2P))));

            p2p.IsDeleted = true;

            if (!await SaveChangesAsync())
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel{});
                
        }

        public IActionResult ListAll()
        {
            if(!_environment.IsDevelopment())
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel("Development only endpoint")));

            var p2ps = _context.P2p.ToList();

            return new OkObjectResult(new ResponseModel{
                Object = p2ps
            });
        }
        private async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}