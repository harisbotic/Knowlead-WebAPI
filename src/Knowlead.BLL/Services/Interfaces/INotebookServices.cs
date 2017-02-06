using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;
using Knowlead.DTO.LibraryModels;
using Microsoft.AspNetCore.JsonPatch;

namespace Knowlead.Services.Interfaces
{
    public interface INotebookServices
    {
        Task<Notebook> Get (int notebookId);
        Task<Notebook> Create (Guid applicationUserId, CreateNotebookModel createNotebookModel);
        Task<Notebook> Patch (int notebookId, JsonPatchDocument<NotebookModel> notebookPatch);
    }
}
