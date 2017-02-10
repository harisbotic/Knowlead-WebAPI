using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.P2PModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IP2PRepository
    {
        Task<P2P> GetP2PTemp(int p2pId);
        Task<IActionResult> Create(P2PModel p2pModel, ApplicationUser applicationUser);
        Task<IActionResult> Schedule(int p2pMessageId, Guid applicationUserId);
        Task<IActionResult> Message(P2PMessageModel p2pMessageModel, ApplicationUser applicationUser);
        Task<IActionResult> Delete(int p2pId, ApplicationUser applicationUser);
        Task<IActionResult> GetP2P(int p2pId);
        Task<IActionResult> GetMessages(int p2pId, ApplicationUser applicationUser);
        Task<IActionResult> ListMine(ApplicationUser applicationUser);
        Task<IActionResult> ListSchedulded(ApplicationUser applicationUser);
        Task<IActionResult> ListBookmarked(ApplicationUser applicationUser);
        Task<IActionResult> ListDeleted(ApplicationUser applicationUser);
        Task<IActionResult> ListActionRequired(ApplicationUser applicationUser);
        Task<IActionResult> ListAll();
    }
}