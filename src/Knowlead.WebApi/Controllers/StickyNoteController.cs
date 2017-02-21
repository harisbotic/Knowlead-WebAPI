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
    public class StickyNoteController : Controller
    {
        private readonly IStickyNoteServices _stickyNoteServices;
        private readonly Auth _auth;

        public StickyNoteController(IStickyNoteServices stickyNoteServices, Auth auth)
        {
            _stickyNoteServices = stickyNoteServices;
            _auth = auth;
        }

        [HttpGet("{stickyNoteId}")]
        public async Task<IActionResult> Get(int stickyNoteId)
        {
            var applicationUserId = _auth.GetUserId();

            var stickyNote = await _stickyNoteServices.Get(applicationUserId, stickyNoteId);

            if(stickyNote == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<StickyNoteModel>(stickyNote)
            });
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var applicationUserId = _auth.GetUserId();

            var stickyNote = await _stickyNoteServices.GetAllFromUser(applicationUserId);

            if(stickyNote == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<List<StickyNoteModel>>(stickyNote)
            });
        }

        [HttpPost(""), ValidateModel]
        public async Task<IActionResult> Create([FromBody] StickyNoteModel stickyNoteModel)
        {
            var applicationUserId = _auth.GetUserId();

            var stickyNote = await _stickyNoteServices.Create(applicationUserId, stickyNoteModel);

            if(stickyNote == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<StickyNoteModel>(stickyNote)
            });
        }
        
        [HttpPost("patch/{stickyNoteId}")] //ValidateModelAttribute?
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<StickyNoteModel> stickyNotePatch, int stickyNoteId)
        {
            var applicationUser = await _auth.GetUser();

            var stickyNote = await _stickyNoteServices.Patch(stickyNoteId, stickyNotePatch);

            if(stickyNote == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<StickyNoteModel>(stickyNote)
            });
        }

        [HttpDelete("{stickyNoteId}")]
        public async Task<IActionResult> Delete(int stickyNoteId)
        {
            var applicationUserId = _auth.GetUserId();

            var isSuccessful = await _stickyNoteServices.Delete(applicationUserId, stickyNoteId);

            if(isSuccessful)
                return Ok(new ResponseModel());

            return BadRequest(new ResponseModel());
        }
    }
}