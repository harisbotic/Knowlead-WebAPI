using System;
using System.Threading.Tasks;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> InsertAccountTransaction(Guid applicationUserId, int minutesChange, int pointsChange, String reason);
    }
}