using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DTO.P2PModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IP2PRepository
    {
        Task<P2P> GetP2PTemp(int p2pId, Guid? applicationUserId = null);
        Task<IActionResult> Create(P2PModel p2pModel, Guid applicationUserId);
        Task<IActionResult> Schedule(int p2pMessageId, Guid applicationUserId);
        Task<IActionResult> Message(P2PMessageModel p2pMessageModel, Guid applicationUserId);
        Task<IActionResult> AcceptOffer(int p2pMessageId, Guid applicationUserId);
        Task<bool> AddBookmark(int p2pId, Guid applicationUserId);
        Task<bool> RemoveBookmark(int p2pId, Guid applicationUserId);
        Task<IActionResult> Delete(int p2pId, Guid applicationUserId);
        Task UpdateAndSave(P2P p2p);
        Task<IActionResult> GetP2P(int p2pId, Guid applicationUserId);
        Task<IActionResult> GetMessages(int p2pId, Guid applicationUserId);
        Task<IActionResult> ListMine(Guid applicationUserId);
        Task<IActionResult> ListSchedulded(Guid applicationUserId);
        Task<IActionResult> ListBookmarked(Guid applicationUserId);
        Task<IActionResult> ListDeleted(Guid applicationUserId);
        Task<IActionResult> ListActionRequired(Guid applicationUserId);
        Task<IActionResult> ListAll(Guid applicationUserId);
    }
}