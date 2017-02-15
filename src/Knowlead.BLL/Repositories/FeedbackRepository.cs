using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.FeedbackModels;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using Microsoft.EntityFrameworkCore;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<P2PFeedback> GetWhereAsync(Expression<Func<P2PFeedback, bool>> condition)
        {
            return await _context.P2PFeedbacks.Where(condition).FirstOrDefaultAsync();
        }

        public async Task<List<_Feedback>> GetAllWhere(Expression<Func<_Feedback, bool>> condition)
        {
            return await _context.Feedbacks.Where(condition).ToListAsync();
        }

        public void Add(P2PFeedback feedback) => _context.Add(feedback);
        public void Add(QuestionFeedback feedback) => _context.Add(feedback);
        public void Add(CourseFeedback feedback) => _context.Add(feedback);
        public void Add(ClassFeedback feedback) => _context.Add(feedback);
        

        public async Task Commit()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}