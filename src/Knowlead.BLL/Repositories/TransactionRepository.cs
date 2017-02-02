using System;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.TransactionModels;
using Microsoft.EntityFrameworkCore;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<bool> InsertAccountTransaction(Guid applicationUserId, int minutesChange, int pointsChange, String reason)
        {
            var accTransaction = new AccountTransaction();
            accTransaction.ApplicationUserId = applicationUserId;
            accTransaction.Reason = reason;

            var prevTransations = await _context.AccountTransactions.Where(x => x.ApplicationUserId.Equals(applicationUserId)).ToListAsync();
            var currentMinutes = prevTransations.Sum(x => x.MinutesChangeAmount);
            var currentPoints = prevTransations.Sum(x => x.PointsChangeAmount);

            if(currentMinutes + minutesChange < 0)
                throw new ErrorModelException(ErrorCodes.NotEnoughMinutes, currentMinutes.ToString());

            accTransaction.MinutesChangeAmount = minutesChange;
            accTransaction.FinalMinutesBalance = currentMinutes + minutesChange;

            accTransaction.PointsChangeAmount = pointsChange;
            accTransaction.FinalPointsBalance = currentPoints + pointsChange;

            _context.AccountTransactions.Add(accTransaction);
            await SaveChangesAsync();

            return true;
        }

        private async Task SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}