using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.P2PModels;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DomainModel.P2PModels;
using Microsoft.AspNetCore.Authorization;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class P2PController : Controller
    {
        private readonly IP2PRepository _p2pRepository;
        private readonly Auth _auth;

        public P2PController(IP2PRepository p2pRespository,
                             Auth auth)
        {
            _p2pRepository = p2pRespository;
            _auth = auth;
        }

        [HttpGet("{p2pId}")]
        public async Task<IActionResult> GetP2P(int p2pId)
        {
            return await _p2pRepository.GetP2P(p2pId);
        }

        [HttpGet("messages/{p2pId}")]
        public async Task<IActionResult> GetMessages(int p2pId)
        {
            var applicationUser = await _auth.GetUser();

            return await _p2pRepository.GetMessages(p2pId, applicationUser);
        }

        [HttpPost("create"), ValidateModel]
        public async Task<IActionResult> Create([FromBody] P2PModel p2pModel)
        {
            var applicationUser = await _auth.GetUser();

            return await _p2pRepository.Create(p2pModel, applicationUser);
        }

        [HttpPost("schedule"), ValidateModel]
        public async Task<IActionResult> Schedule([FromBody] P2PScheduleModel p2pScheduleModel)
        {
            var applicationUser = await _auth.GetUser();

            return await _p2pRepository.Schedule(p2pScheduleModel, applicationUser);
        }

        [HttpPost("message"), ValidateModel]
        public async Task<IActionResult> Message([FromBody] P2PMessageModel p2pMessageModel)
        {
            var applicationUser = await _auth.GetUser();

            return await _p2pRepository.Message(p2pMessageModel, applicationUser);
        }

        [HttpDelete("delete/{p2pId}")]
        public async Task<IActionResult> Delete(int p2pId)
        {
            var applicationUser = await _auth.GetUser();

            return await _p2pRepository.Delete(p2pId, applicationUser);
        }

        [HttpGet("my")]
        public async Task<IActionResult> ListMine()
        {
            return await _p2pRepository.ListMine(await _auth.GetUser());
        }

        [HttpGet("bookmarked")]
        public async Task<IActionResult> ListBookmarked()
        {
            await _p2pRepository.ListBookmarked(await _auth.GetUser());
            return BadRequest();
        }
        
        [HttpGet("scheduled")]
        public async Task<IActionResult> ListSchedulded()
        {
            return await _p2pRepository.ListSchedulded(await _auth.GetUser());
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> ListDeleted()
        {
            return await _p2pRepository.ListDeleted(await _auth.GetUser());
        }

        [HttpGet("actionrequired")]
        public async Task<IActionResult> ListActionRequired()
        {
            return await _p2pRepository.ListActionRequired(await _auth.GetUser());
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return await _p2pRepository.ListAll();
        }
    }
}