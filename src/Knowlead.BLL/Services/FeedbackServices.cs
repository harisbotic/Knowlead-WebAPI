using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using Knowlead.DTO.LookupModels.FeedbackModels;
using Knowlead.Services.Interfaces;
using static Knowlead.Common.Constants;

namespace Knowlead.Services
{
    public class FeedbackServices : IFeedbackServices
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IP2PRepository _p2pRepository;
        private readonly IAccountRepository _accountRepository;

        public FeedbackServices(IFeedbackRepository feedbackRepository, IP2PRepository p2pRepository, IAccountRepository accountRepository)
        {
            _feedbackRepository = feedbackRepository;
            _p2pRepository = p2pRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> Exists(Expression<Func<P2PFeedback, bool>> condition)
        {
            return await _feedbackRepository.GetWhereAsync(condition) != null;
        }

        public async Task<P2PFeedback> GiveP2PFeedback(P2PFeedbackModel p2pFeedbackModel, Guid applicationUserId)
        {
            var p2pFeedback = Mapper.Map<P2PFeedbackModel, P2PFeedback>(p2pFeedbackModel);
            var p2p = await _p2pRepository.GetP2PTemp(p2pFeedback.P2pId);

            // if(p2p.Status != P2PStatus.Finsihed) //Had to had call
            //     throw new ErrorModelException("");

            if(!p2p.CreatedById.Equals(applicationUserId)) //Only creator can give feedback
                throw new ErrorModelException(ErrorCodes.HackAttempt);

            // if(await Exists(x => x.P2pId.Equals(p2p.P2pId))) //Is Feedback Already given
            //     throw new ErrorModelException(ErrorCodes.FeedbackAlreadyGiven, nameof(P2PFeedback));

            p2pFeedback.StudentId = p2p.CreatedById;
            p2pFeedback.TeacherId = p2p.ScheduledWithId.GetValueOrDefault();
            p2pFeedback.FosId = p2p.FosId;

            p2pFeedback.Expertise.LimitToRange(0,5);
            p2pFeedback.Helpful.LimitToRange(0,5);

            _feedbackRepository.Add(p2pFeedback);
            await _accountRepository.UpdateUserRating(applicationUserId);
            await _feedbackRepository.Commit();

            return p2pFeedback;
        }
    }
}