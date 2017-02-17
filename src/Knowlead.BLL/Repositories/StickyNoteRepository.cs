using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.LibraryModels;
using Microsoft.EntityFrameworkCore;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class StickyNoteRepository : IStickyNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public StickyNoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StickyNote> Get(int stickyNoteId)
        {
            return await _context.StickyNotes.Where(x => x.StickyNoteId.Equals(stickyNoteId)).FirstOrDefaultAsync();
        }

        public async Task<List<StickyNote>> GetAllWhere(Expression<Func<StickyNote, bool>> condition)
        {
            return await _context.StickyNotes.Where(condition).Where(x => x.IsDeleted == false).ToListAsync();
        }

        public void Add(StickyNote stickyNote)
        {
            _context.Add(stickyNote);
        }

        public void Update(StickyNote stickyNote)
        {
            _context.Update(stickyNote);
        }

        public async Task Commit()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}