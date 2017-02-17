using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;
using Knowlead.DTO.LibraryModels;
using Microsoft.AspNetCore.JsonPatch;

namespace Knowlead.Services.Interfaces
{
    public interface IStickyNoteServices
    {
        Task<StickyNote> Get(Guid applicationUserId, int stickyNoteId);
        Task<List<StickyNote>> GetAllFromUser(Guid applicationUserId);
        Task<StickyNote> Create(Guid applicationUserId, StickyNoteModel stickyNoteModel);
        Task<StickyNote> Patch(int stickyNoteId, JsonPatchDocument<StickyNoteModel> stickyNotePatch);
        Task<Boolean> Delete(Guid applicationUserId, int stickyNoteId);
    }
}
