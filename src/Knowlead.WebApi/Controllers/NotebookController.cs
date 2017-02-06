using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Attributes;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.LibraryModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class NotebookController : Controller
    {
        private readonly INotebookRepository _notebookRepository;
        private readonly INotebookServices _notebookServices;
        private readonly Auth _auth;

        public NotebookController(INotebookRepository notebookRepository, INotebookServices notebookServices, Auth auth)
        {
            _notebookRepository = notebookRepository;
            _notebookServices = notebookServices;
            _auth = auth;
        }

        [HttpPost("{notebookId}")]
        public async Task<IActionResult> Get(int notebookId)
        {
            var applicationUserId = _auth.GetUserId();

            var notebook = await _notebookServices.Get(notebookId);

            if(notebook == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<NotebookModel>(notebook)
            });
        }

        [HttpPost(""), ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateNotebookModel createNotebookModel)
        {
            var applicationUserId = _auth.GetUserId();

            var notebook = await _notebookServices.Create(applicationUserId, createNotebookModel);

            if(notebook == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<NotebookModel>(notebook)
            });
        }
        
        [HttpPatch("{notebookId}")] //ValidateModelAttribute?
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<NotebookModel> notebookPatch, int notebookId)
        {
            var applicationUser = await _auth.GetUser();

            var notebook = _notebookServices.Patch(notebookId, notebookPatch);

            if(notebook == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<NotebookModel>(notebook)
            });
        }
    }
}