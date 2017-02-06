using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;
using Knowlead.DTO.LibraryModels;
using Microsoft.AspNetCore.JsonPatch;

namespace Knowlead.Services.Interfaces
{
    public interface INotebookServices
    {
        Task<Notebook> Get (Guid applicationUserId, int notebookId);
        Task<List<Notebook>> GetAllFromUser (Guid applicationUserId);
        Task<Notebook> Create (Guid applicationUserId, NotebookModel notebookModel);
        Task<Notebook> Patch (int notebookId, JsonPatchDocument<NotebookModel> notebookPatch);
        Task<Notebook> Send(int notebookId, Guid fromUser, Guid toUser);
    }
}
