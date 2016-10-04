using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.UserModels
{
    public class RegisterUserModel
    {
        [MyRequired]
        [MyEmailAddress]
        public string Email { get; set; }
        [MyRequired]
        public string Password { get; set; }
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
}
