using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Http;
using static Knowlead.Common.Constants;

namespace Knowlead.Common.HttpRequestItems
{
    public class Auth
    {
        private const string UserKey = "Knowlead.Auth.UserKey";
    
        private readonly IHttpContextAccessor _accessor;
        private readonly IAccountRepository _accountRepository;

        public Auth(IHttpContextAccessor accessor,
                    IAccountRepository accountRepository)
        {
            _accessor = accessor;
            _accountRepository = accountRepository;
        }
        public async Task<ApplicationUser> GetUser(bool includeDetails = false)
        {
            if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
                throw new ErrorModelException(ErrorCodes.NotLoggedIn);

            var user = _accessor.HttpContext.Items[UserKey] as ApplicationUser;
            if (user == null)
            {
                var userId = GetUserId();

                user = await _accountRepository.GetApplicationUserById(userId, includeDetails);

                if (user == null)
                    return null;

                _accessor.HttpContext.Items[UserKey] = user;
            }
            return user;
        }

        public Guid GetUserId()
        {
            if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
                throw new ErrorModelException(ErrorCodes.NotLoggedIn);

            return _accessor.HttpContext.User.Claims.GetUserIdFromClaims();

        }
    }
}