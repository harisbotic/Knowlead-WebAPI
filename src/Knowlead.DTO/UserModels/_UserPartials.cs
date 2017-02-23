using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.UserModels
{
    public class RegisterUserModel //TODO: Rename those to RegisterUserFormModel or just Form
    {
        [MyRequired]
        [MyEmailAddress]
        public string Email { get; set; }
        [MyRequired]
        public string Password { get; set; }
        public Guid? ReferralUserId { get; set; }
    }

    public class ConfirmEmailModel
    {
        [MyRequired]
        [MyEmailAddress]
        public string Email { get; set; }
        [MyRequired]
        public string Code { get; set; }
        [MyRequired]
        public string Password { get; set; }
    }

    public class PasswordResetModel
    {
        [MyRequired]
        [MyEmailAddress]
        public string Email { get; set; }
        [MyRequired]
        public string Token { get; set; }
        [MyRequired]
        public string NewPassword { get; set; }
    }
}
