using Microsoft.AspNetCore.Identity;
namespace Knowlead.WebApi.Config
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
        {
            private const string email = "email";
            private const string password = "password";
            private const string username = "username";
            private const string role = "role";

            public override IdentityError DefaultError() { return new IdentityError { Description = $"An unknown failure has occurred." }; }
            public override IdentityError ConcurrencyFailure() { return new IdentityError { Description = "Optimistic concurrency failure, object has been modified." }; }
            public override IdentityError PasswordMismatch() { return new IdentityError { Code = password, Description = "Incorrect password." }; }
            public override IdentityError InvalidToken() { return new IdentityError { Description = "Invalid token." }; }
            public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Description = "A user with this login already exists." }; }
            public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = username, Description = $"User name '{userName}' is invalid, can only contain letters or digits." }; }
            public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = email, Description = $"Email '{email}' is invalid."  }; }
            public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = username, Description = $"User Name '{userName}' is already taken."  }; }
            public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = email, Description = $"Email '{email}' is already taken."  }; }
            public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = role, Description = $"Role name '{role}' is invalid."  }; }
            public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = role, Description = $"Role name '{role}' is already taken."  }; }
            public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = password, Description = "User already has a password set." }; }
            public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Description = "Lockout is not enabled for this user." }; }
            public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = role, Description = $"User already in role '{role}'."  }; }
            public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = role, Description = $"User is not in role '{role}'."  }; }
            public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = password, Description = $"Passwords must be at least {length} characters."  }; }
            public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = password, Description = "Passwords must have at least one non alphanumeric character." }; }
            public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = password, Description = "Passwords must have at least one digit ('0'-'9')." }; }
            public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = password, Description = "Passwords must have at least one lowercase ('a'-'z')." }; }
            public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = password, Description = "Passwords must have at least one uppercase ('A'-'Z')." }; }
        }
}