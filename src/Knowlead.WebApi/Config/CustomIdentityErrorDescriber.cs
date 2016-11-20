using Microsoft.AspNetCore.Identity;
using static Knowlead.Common.Constants.ErrorCodes;

namespace Knowlead.WebApi.Config
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
        {
            private const string myEmail = "email";
            private const string myPassword = "password";
            private const string myUsername = "username";
            private const string myRole = "role";

            public override IdentityError DefaultError() { return new IdentityError { Description = $"An unknown failure has occurred." }; }
            public override IdentityError ConcurrencyFailure() { return new IdentityError { Description = "Optimistic concurrency failure, object has been modified." }; }
            public override IdentityError PasswordMismatch() { return new IdentityError { Code = myPassword, Description = PasswordIncorrect }; }
            public override IdentityError InvalidToken() { return new IdentityError { Description = TokenInvalid }; }
            public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Description = "A user with this login already exists." }; }
            public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = myUsername, Description = $"User name '{userName}' is invalid, can only contain letters or digits." }; }
            public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = myEmail, Description = $"{EmailInvalid}:{email}" }; }
            public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = null, Description = null }; }
            public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = myEmail, Description = $"{EmailTaken}:{email}"}; }
            public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = role, Description = $"Role name '{role}' is invalid."  }; }
            public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = role, Description = $"Role name '{role}' is already taken."  }; }
            public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = myPassword, Description = "User already has a password set." }; }
            public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Description = "Lockout is not enabled for this user." }; }
            public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = role, Description = $"User already in role '{role}'."  }; }
            public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = role, Description = $"User is not in role '{role}'."  }; }
            public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = myPassword, Description = $"{TooShort}:{length}" }; }
            public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = myPassword, Description = RequiresNonAlphanumeric }; }
            public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = myPassword, Description = RequiresDigit }; }
            public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = myPassword, Description = RequiresLowercase }; }
            public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = myPassword, Description = RequiresUppercase}; }
        }
}