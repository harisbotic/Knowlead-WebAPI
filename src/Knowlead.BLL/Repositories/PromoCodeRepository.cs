using System;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Knowlead.Common.Constants.EnumStatuses;
using static Knowlead.Common.Utils;
using Knowlead.DomainModel.ChatModels;
using Knowlead.Common.Exceptions;
using Knowlead.Services;
using Knowlead.DomainModel;
using Knowlead.Services.Interfaces;

namespace Knowlead.BLL.Repositories
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRewardServices _rewardServices;

        public PromoCodeRepository(ApplicationDbContext context, IRewardServices rewardServices)
        {
            _context = context;
            _rewardServices = rewardServices;
        }

        public async Task<PromoCode> ApplyPromoCode(string code, Guid applicationUserId)
        {
            var promoCode = await _context.PromoCodes.Where(x => x.Code.Equals(code)).FirstOrDefaultAsync();

            if(promoCode == null)
                throw new ErrorModelException(ErrorCodes.PromoCodeInvalid);
            
            if(promoCode.ActivatorId != null)
                throw new ErrorModelException(ErrorCodes.PromoCodeAlreadyUsed);

            if(promoCode.ExpirationDate <= DateTime.UtcNow)
                throw new ErrorModelException(ErrorCodes.PromoCodeExpired);

            promoCode.ActivatedAt = DateTime.UtcNow;
            promoCode.ActivatorId = applicationUserId;

            _context.PromoCodes.Update(promoCode);

            await _rewardServices.ClaimReward(applicationUserId, promoCode.RewardId);

            return promoCode;
        }
    }
}