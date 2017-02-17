using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly INotebookServices _notebookServices;
        private readonly Auth _auth;

        public NotebookController(INotebookServices notebookServices, Auth auth)
        {
            _notebookServices = notebookServices;
            _auth = auth;
        }

        [HttpGet("{notebookId}")]
        public async Task<IActionResult> Get(int notebookId)
        {
            var applicationUserId = _auth.GetUserId();

            var notebook = await _notebookServices.Get(applicationUserId, notebookId);

            if(notebook == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<NotebookModel>(notebook)
            });
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var applicationUserId = _auth.GetUserId();

            var notebook = await _notebookServices.GetAllFromUser(applicationUserId);

            if(notebook == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<List<NotebookModel>>(notebook)
            });
        }

        [HttpPost(""), ValidateModel]
        public async Task<IActionResult> Create([FromBody] NotebookModel notebookModel)
        {
            var applicationUserId = _auth.GetUserId();

            var notebook = await _notebookServices.Create(applicationUserId, notebookModel);

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

            var notebook = await _notebookServices.Patch(notebookId, notebookPatch);

            if(notebook == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<NotebookModel>(notebook)
            });
        }

        [HttpDelete("{notebookId}")]
        public async Task<IActionResult> Delete(int notebookId)
        {
            var applicationUserId = _auth.GetUserId();

            var isSuccessful = await _notebookServices.Delete(applicationUserId, notebookId);

            if(isSuccessful)
                return Ok(new ResponseModel());

            return BadRequest(new ResponseModel());
        }
    }
}