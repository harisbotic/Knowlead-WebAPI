using System;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Interfaces;
using Knowlead.Common;
using Knowlead.DAL;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL
{
    public class P2PRepository : IP2PRepository
    {
        private ApplicationDbContext _context;

        public P2PRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(P2PModel p2pModel, ApplicationUser applicationUser)
        {
            var p2p = Mapper.Map<P2P>(p2pModel);

            p2p.CreatedById = applicationUser.Id;
            
            if(p2p.Deadline != null && p2p.Deadline < DateTime.Now.AddMinutes(1))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(P2P.Deadline), Constants.ErrorCodes.IncorrectValue)));

            _context.P2p.Add(p2p);
            return await SaveChangesAsync();

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