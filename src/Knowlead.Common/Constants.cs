using TypeScriptBuilder;

namespace Knowlead.Common
{
    public static class Constants
    {
        [TSClass]
        public static class ErrorCodes
        {
            [TSInitialize]
            public const string RequiredField = "REQUIRED_FIELD";
            [TSInitialize]
            public const string DatabaseError = "DATABASE_ERROR";
            [TSInitialize]
            public const string TokenInvalid = "TOKEN_INVALID";
            [TSInitialize]
            public const string EmailInvalid = "EMAIL_INVALID";
            [TSInitialize]
            public const string EmailTaken = "EMAIL_TAKEN";
            [TSInitialize]
            public const string EmailNotVerified = "EMAIL_NOT_VERIFIED";
            [TSInitialize]
            public const string ModelEmpty = "MODEL_EMPTY";
            [TSInitialize]
            public const string EntityNotFound = "ENTITY_NOT_FOUND";
            [TSInitialize]
            public const string PasswordIncorrect = "PASSWORD_INCORRECT";
            [TSInitialize]
            public const string TooShort = "TOO_SHORT";
            [TSInitialize]
            public const string RequiresNonAlphanumeric = "REQUIRES_NON_ALPHANUMERIC";
            [TSInitialize]
            public const string RequiresDigit = "REQUIRES_DIGIT";
            [TSInitialize]
            public const string RequiresUppercase = "REQUIRES_UPPERCASE";
            [TSInitialize]
            public const string RequiresLowercase = "REQUIRES_LOWERCASE";
            [TSInitialize]
            public const string AlreadyDeleted = "ALREADY_DELETED";
            [TSInitialize]
            public const string ConfirmationCodeIncorrect = "CONFIRMATION_CODE_INCORRECT";
            [TSInitialize]
            public const string NotLoggedIn = "NOT_LOGGED_IN";
            [TSInitialize]
            public const string LoginCredentialsIncorrect = "LOGIN_CREDENTIALS_INCORRECT";
            [TSInitialize]
            public const string IncorrectValue = "INCORRECT_VALUE";
            [TSInitialize]
            public const string OwnershipError = "OWNERSHIP_ERROR";
            [TSInitialize]
            public const string AuthorityError = "AUTHORITY_ERROR";
            [TSInitialize]
            public const string HackAttempt = "HACK_ATTEMPT";

        }

    }
}