using System;
using System.Threading.Tasks;

namespace Knowlead.Services.Interfaces
{
    public interface ITransactionServices
    {
        Task<bool> RewardMinutes(Guid applicationUserId, int minutesAmount, int pointsAmount, String reason);
        Task<bool> PayWithMinutes(Guid applicationUserId, int minutesAmount, int pointsAmount, String reason);
    }
}
