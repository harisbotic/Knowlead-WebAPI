using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.P2PModels;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class P2PController : Controller {
        private readonly Auth _auth;

        public P2PController(Auth auth)
        {
            _auth = auth;
        }

        [HttpPost("create"), ValidateModel]
        public async Task<IActionResult> Create([FromBody] P2PModel p2pModel)
        {
            var applicationUser = await _auth.GetUser();

            return Ok();
        }
    }
}