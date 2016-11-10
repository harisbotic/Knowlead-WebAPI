using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.P2PModels;
using Knowlead.WebApi.Attributes;
using Knowlead.BLL.Repositories.Interfaces;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("create"), ReallyAuthorize, ValidateModel]
        public async Task<IActionResult> Create([FromBody] P2PModel p2pModel)
        {
            var applicationUser = await _auth.GetUser();

            return await _p2pRepository.Create(p2pModel, applicationUser);
        }

        [HttpGet("list"), ReallyAuthorize]
        public IActionResult List()
        {
            return _p2pRepository.ListAll();
        }
    }
}