using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IStickyNoteRepository
    {
        Task<StickyNote> Get (int stickyNoteId);
        Task<List<StickyNote>> GetAllWhere (Expression<Func<StickyNote, bool>> condition);
        void Add (StickyNote stickyNote);
        void Update(StickyNote stickyNote);
        Task Commit ();
    }
}