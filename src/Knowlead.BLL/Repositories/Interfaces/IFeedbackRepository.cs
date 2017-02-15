using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.DomainModel.FeedbackModels;
using Knowlead.DomainModel.LookupModels.FeedbackModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<P2PFeedback> GetWhereAsync(Expression<Func<P2PFeedback, bool>> condition);
        Task<List<_Feedback>> GetAllWhere (Expression<Func<_Feedback, bool>> condition);
        void Add (P2PFeedback feedback);
        void Add (QuestionFeedback feedback);
        void Add (CourseFeedback feedback);
        void Add (ClassFeedback feedback);
        Task Commit ();
    }
}