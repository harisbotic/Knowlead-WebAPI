using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface INotebookRepository
    {
        Task<Notebook> Get (int notebookId);
        Task<List<Notebook>> GetAllWhere (Expression<Func<Notebook, bool>> condition);
        void Add (Notebook notebook);
        void Update(Notebook notebook);
        Task Commit ();
    }
}