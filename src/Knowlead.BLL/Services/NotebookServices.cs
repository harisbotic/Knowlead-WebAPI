using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.LibraryModels;
using Knowlead.DTO.LibraryModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using static Knowlead.Common.Constants;

namespace Knowlead.Services
{
    public class NotebookServices : INotebookServices
    {
        private readonly INotebookRepository _notebookRepository;

        public NotebookServices(INotebookRepository notebookRepository)
        {
            _notebookRepository = notebookRepository;
        }

        public async Task<Notebook> Get(Guid applicationUserId, int notebookId)
        {
            var notebook = await _notebookRepository.Get(notebookId);

            if(notebook == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(Notebook));

            //Check if is allowed to read
            if(!notebook.CreatedById.Equals(applicationUserId))
                throw new ErrorModelException(ErrorCodes.OwnershipError);

            return notebook;
        }

        public async Task<List<Notebook>> GetAllFromUser(Guid applicationUserId)
        {
            var notebooks = await _notebookRepository.GetAllWhere(notebook => notebook.CreatedById.Equals(applicationUserId));

            return notebooks;
        }

        public async Task<Notebook> Create(Guid applicationUserId, NotebookModel notebookModel)
        {
            var notebook = Mapper.Map<Notebook>(notebookModel);
            notebook.CreatedById = applicationUserId;
            
            _notebookRepository.Add(notebook);

            await _notebookRepository.Commit();

            return notebook;
        }

        public async Task<Notebook> Patch(int notebookId, JsonPatchDocument<NotebookModel> notebookPatch)
        {
            var notebook = await _notebookRepository.Get(notebookId);

            if(notebook == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(Notebook));

            var notebookModel = Mapper.Map<NotebookModel>(notebook);
            notebookPatch.ApplyTo(notebookModel);

            notebook = Mapper.Map<NotebookModel, Notebook>(notebookModel, notebook);

            await _notebookRepository.Commit();

            return notebook;
        }

        public async Task<Boolean> Delete(Guid applicationUserId, int notebookId )
        {
            var notebook = await Get(applicationUserId, notebookId);
            notebook.IsDeleted = true;
            
            _notebookRepository.Update(notebook);
            await _notebookRepository.Commit();
            return true;
        }

        public async Task<Notebook> Send(int notebookId, Guid fromUser, Guid toUser)
        {
            var notebookToShare = await Get(fromUser, notebookId);
            
            // var sharedNotebook = await Create(toUser, )
            // _notebookRepository.Add(notebook);

            // await _notebookRepository.Commit();

            // return notebook;
            throw new NotImplementedException();
        }
    }
}