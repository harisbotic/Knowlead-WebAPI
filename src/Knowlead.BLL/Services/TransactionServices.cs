using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Services.Interfaces;

namespace Knowlead.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionServices(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> RewardMinutes(Guid applicationUserId, int minutesAmount, int pointsAmount, String reason)
        {
            return await _transactionRepository.InsertAccountTransaction(applicationUserId, minutesAmount, pointsAmount, reason);
        }

        public async Task<bool> PayWithMinutes(Guid applicationUserId, int minutesAmount, int pointsAmount, String reason)
        {
            return await _transactionRepository.InsertAccountTransaction(applicationUserId, minutesAmount, pointsAmount, reason);
        }

    }
}