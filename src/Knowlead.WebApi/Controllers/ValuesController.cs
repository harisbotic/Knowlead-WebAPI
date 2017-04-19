using Knowlead.Common.Configurations.AppSettings;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DAL;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly Auth _auth;
        private readonly AppSettings _appSettings;

        public ValuesController(UnitOfWork uow, Auth auth, IOptions<AppSettings> appSettings)
        {
            _uow = uow;
            _auth = auth;
            _appSettings = appSettings.Value;
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