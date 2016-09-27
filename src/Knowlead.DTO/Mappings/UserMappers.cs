using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.DomainModels.ApplicationUserModels;
using Knowlead.DTO.SpecificModels.ApplicationUserModels;

namespace Knowlead.DTO.Mappings {
    public static class UserMappers {
        public static ApplicationUserModel MapToApplicationUserModel(this ApplicationUser model)
        {
            return new ApplicationUserModel
            {
                Username = model.UserName,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed
            };
        }
        public static ApplicationUser MapToApplicationUser(this RegisterUserModel model)
        {
            return new ApplicationUser
            {
                Email = model.Email,
                PasswordHash = model.Password,
                UserName = model.Username
            };
        }

        public static UserDetailsModel MapToUserDetailModel(this ApplicationUser applicationUser)
        {
            return new UserDetailsModel
            {
                Name = applicationUser.Name,
                Surname = applicationUser.UserName,
                Birthdate = applicationUser.Birthdate,
                IsMale = applicationUser.IsMale,
                CountryId = applicationUser.CountryId,
                StateId = applicationUser.StateId
            };
        }
    }
}