using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.LibraryModels;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class NotebookRepository : INotebookRepository
    {
        private readonly ApplicationDbContext _context;

        public NotebookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Notebook> Get(int notebookId)
        {
            throw new NotImplementedException();
        }

        public Task<Notebook> Insert(Guid applicationUserId)
        {
            throw new NotImplementedException();
        }

        public void Add(Notebook notebook)
        {
            _context.Add(notebook);
        }

        public async Task Commit()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}