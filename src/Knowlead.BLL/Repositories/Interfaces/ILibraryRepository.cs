using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface INotebookRepository
    {
        Task<Notebook> Get (int notebookId);
        Task<Notebook> Insert (Guid applicationUserId);
        void Add (Notebook notebook);
        Task Commit ();
    }
}