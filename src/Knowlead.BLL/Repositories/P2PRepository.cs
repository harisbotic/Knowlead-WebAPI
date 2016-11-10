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

        public async Task<IActionResult> Create(P2PModel p2pModel, ApplicationUser applicationUser)
        {
            var p2p = Mapper.Map<P2P>(p2pModel);
            p2p.CreatedById = applicationUser.Id;
            
            if(p2p.Deadline != null && p2p.Deadline < DateTime.Now.AddMinutes(1))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(P2P.Deadline), Constants.ErrorCodes.IncorrectValue)));

            foreach (var img in p2pModel.Images)
            {
                p2p.P2PImages.Add(new P2PImage{
                    P2p = p2p,
                    ImageBlobId = img.BlobId
                });
            }

            foreach (var file in p2pModel.Files)
            {
                p2p.P2PFiles.Add(new P2PFile{
                    P2p = p2p,
                    FileBlobId = file.BlobId
                });
            }

            _context.P2p.Add(p2p);
            return await SaveChangesAsync();
        }

        public async Task<IActionResult> Delete(int p2pInt, ApplicationUser applicationUser)
        {
            var p2p = _context.P2p.Where(x => x.P2pId == p2pInt).FirstOrDefault();

            if (p2p == null)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.P2PNotFound)));

            if(p2p.CreatedById != applicationUser.Id)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.OwnershipProblem)));

            if(p2p.IsDeleted)
                return new BadRequestObjectResult(new ResponseModel(new ErrorModel(Common.Constants.ErrorCodes.P2PAlreadyDeleted)));

            p2p.IsDeleted = true;
            return await SaveChangesAsync();
                
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
        private async Task<IActionResult> SaveChangesAsync()
        {
            bool hasChanges = (await _context.SaveChangesAsync() > 0);

            if (!hasChanges)
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel());
        }
    }
}