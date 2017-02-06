using System;
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
        private readonly ITransactionServices _transactionServices;
        private readonly INotificationServices _notificationServices;
        private readonly IAccountRepository _accountRepository;

        public NotebookServices(INotebookRepository notebookRepository, ITransactionServices transactionServices,
                              INotificationServices notificationServices, IAccountRepository accountRepository)
        {
            _notebookRepository = notebookRepository;
            _transactionServices = transactionServices;
            _notificationServices = notificationServices;
            _accountRepository = accountRepository;
        }

        public async Task<Notebook> Get(int notebookId)
        {
            var notebook = await _notebookRepository.Get(notebookId);

            return notebook;
        }

        public async Task<Notebook> Create(Guid applicationUserId, CreateNotebookModel createNotebookModel)
        {
            var notebook = Mapper.Map<Notebook>(createNotebookModel);
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
    }
}