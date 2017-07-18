using System;
using System.Threading.Tasks;
using Knowlead.DomainModel;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IPromoCodeRepository
    {
        Task<PromoCode> ApplyPromoCode(string promoCode, Guid applicationUserId);
    }
}