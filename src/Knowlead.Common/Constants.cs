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
            public const string EmailInvalid = "EMAIL_INVALID";
            [TSInitialize]
            public const string EmailNotVerified = "EMAIL_NOT_VERIFIED";
            [TSInitialize]
            public const string ModelEmpty = "MODEL_EMPTY";
            [TSInitialize]
            public const string UserNotFound = "USER_NOT_FOUND";
            [TSInitialize]
            public const string PasswordIncorrect = "INCORRECT_PASSWORD";
            [TSInitialize]
            public const string ConfirmationCodeIncorrect = "INCORRECT_CODE";
            [TSInitialize]
            public const string NotLoggedIn = "NOT_LOGGED_IN";
        }

    }
}