using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ApplicationUserModels;

namespace Knowlead.DTO.Mappers {
    static class UserMappers {
        public static ApplicationUser MapToApplicationUser(this RegisterUserModel model) {
            return new ApplicationUser{Email = model.Email, PasswordHash = model.Password, UserName = model.Username};
        }

        public static ApplicationUserModel MapToApplicationUserModel(this ApplicationUser model) {
            return new ApplicationUserModel{
                Username = model.UserName,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed
            };
        }
    }
}