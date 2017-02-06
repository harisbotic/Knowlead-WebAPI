using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface INotebookRepository
    {
        Task<Notebook> Get (int notebookId);
        Task<List<Notebook>> GetAllWhere (Func<Notebook, bool> condition);
        void Add (Notebook notebook);
        Task Commit ();
    }
}