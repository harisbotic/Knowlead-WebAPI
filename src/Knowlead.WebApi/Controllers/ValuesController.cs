using System.Threading.Tasks;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DAL;
using Knowlead.DTO.ResponseModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class ValuesController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly Auth _auth;

        public ValuesController(UnitOfWork uow, Auth auth)
        {
            _uow = uow;
            _auth = auth;
        }
        
        [HttpGet("value")]
        public IActionResult ReferralStats()
        {
            //var e = _uow.CourseRepository.Get(x => x.NotebookId == 1);
            

            return Ok(new ResponseModel()
            {
            });
        }
    }
}