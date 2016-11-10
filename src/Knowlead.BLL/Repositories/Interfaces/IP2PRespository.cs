using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.P2PModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IP2PRepository
    {
        Task<IActionResult> Create(P2PModel p2pModel, ApplicationUser applicationUser);
        Task<IActionResult> Delete(int p2pId, ApplicationUser applicationUser);
        IActionResult ListAll();
    }
}