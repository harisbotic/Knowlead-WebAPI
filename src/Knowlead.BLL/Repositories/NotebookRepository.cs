using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.LibraryModels;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Notebook> Get(int notebookId)
        {
            return await _context.Notebooks.Where(x => x.NotebookId.Equals(notebookId)).FirstOrDefaultAsync();
        }

        public async Task<List<Notebook>> GetAllWhere(Func<Notebook, bool> condition)
        {
            return await _context.Notebooks.Where(x => condition(x)).ToListAsync();
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