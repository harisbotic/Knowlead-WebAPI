using TypeScriptBuilder;

namespace Knowlead.Common
{
    public static class Constants
    {
        [TSClass]
        public static class ErrorCodes //TODO: Turn into enums??
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
            public const string SthWentWrong = "STH_WENT_WRONG";
            [TSInitialize]
            public const string IncorrectValue = "INCORRECT_VALUE";
            [TSInitialize]
            public const string OwnershipError = "OWNERSHIP_ERROR";
            [TSInitialize]
            public const string AuthorityError = "AUTHORITY_ERROR";
            [TSInitialize]
            public const string HackAttempt = "HACK_ATTEMPT";
            [TSInitialize]
            public const string AgeTooYoung = "AGE_TOO_YOUNG";
            [TSInitialize]
            public const string AgeTooOld = "AGE_TOO_OLD";
            [TSInitialize]
            public const string DiscussionClosed = "DISCUSSION_CLOSED";
            [TSInitialize]
            public const string AlreadyScheduled = "ALREADY_SCHEDULED";
            [TSInitialize]
            public const string DateInPast = "DATE_IN_PAST";
            [TSInitialize]
            public const string UserBlocked = "USER_BLOCKED";
            [TSInitialize]
            public const string AlreadyFriends = "ALREADY_FRIENDS";
            [TSInitialize]
            public const string RequestNotFound = "REQUEST_NOT_FOUND";
            [TSInitialize]
            public const string NotInFriendlist = "NOT_IN_FRIENDLIST";
            [TSInitialize]
            public const string ClaimRewardError = "CLAIM_REWARD_ERROR";
            [TSInitialize]
            public const string AlreadyGotReward = "ALREADY_GOT_REWARD";
            [TSInitialize]
            public const string NotEnoughMinutes = "NOT_ENOUGH_MINUTES";
            [TSInitialize]
            public const string ConsecutiveOffersLimit = "CONSECUTIVE_OFFERS_LIMIT";
            [TSInitialize]
            public const string NotLastOffer = "NOT_LAST_OFFER";   
            [TSInitialize]
            public const string AlreadyInCall = "ALREADY_IN_CALL";
            [TSInitialize]
            public const string FeedbackAlreadyGiven = "FEEDBACK_ALREADY_GIVEN";
            [TSInitialize]
            public const string AlreadyBookmarked = "ALREADY_BOOKMARKED";
            [TSInitialize]
            public const string WasntBookmarked = "WASNT_BOOKMARKED";
        }

        [TSClass]
        public static class CallEndReasons
        {
            [TSInitialize]
            public const string Requested = "CALL_END_REQUESTED";
            [TSInitialize]
            public const string Inactive = "CALL_INACTIVE";
            [TSInitialize]
            public const string Rejected = "CALL_REJECTED";
            [TSInitialize]
            public const string StartProblem = "CALL_START_PROBLEM";
        }

        [TSClass]
        public static class NotificationTypes
        {
            [TSInitialize]
            public const string NewP2PComment = "NotificationTypes.NEW_P2P_COMMENT";
            [TSInitialize]
            public const string P2PExpired = "NotificationTypes.P2P_EXPIRED";
            [TSInitialize]
            public const string P2POfferAccepted = "NotificationTypes.P2P_OFFER_ACCEPTED";
            [TSInitialize]
            public const string PrepareForP2P = "NotificationTypes.PREPARE_FOR_P2P";
            [TSInitialize]
            public const string RewardClaimed = "NotificationTypes.REWARD_CLAIMED";
            [TSInitialize]
            public const string NewFriendship = "NotificationTypes.NEW_FRIENDSHIP";
        }

        [TSClass]
        public static class EnumStatuses
        {
            public enum UserStatus 
            {
                Online = 0, Offline = 1, Busy = 2
            }

            public enum P2PStatus 
            {
                Inactive = 0, Active = 1, Scheduled = 2, Finsihed = 3
            }

            //Call Status ? Paused, InProgress, WaitingFor
            public enum PeerStatus 
            {
                Accepted = 0, Rejected = 1, Waiting = 2, Disconnected = 3
            }

            public enum FriendshipStatus 
            {
                Pending = 0, Accepted = 1, Declined = 2, Blocked = 3
            }
        }

        [TSClass]
        public static class EnumActions
        {
            public enum FriendshipDTOActions 
            {
                NewRequest = 0, AcceptRequest = 1, DeclineRequest = 2, CancelRequest = 3,
                RemoveFriend = 4, BlockUser = 5, UnblockUser = 6
            }

            public enum ListP2PsRequest
            {
                My = 0, Scheduled = 1, Bookmarked = 2, ActionRequired = 3, Deleted = 4
            }

            public enum FeedbackOptionsRequest
            {
                P2P = 0, Question = 1, Course = 2, Class = 3, Video = 4
            }
        }

        [TSClass]
        public static class CustomClaimTypes
        {
            [TSInitialize]
            public const string Permission = "CustomClaimTypes.Permission";
        }

        [TSClass]
        public static class Policies
        {
            [TSInitialize]
            public const string RegisteredUser = "Policies.RegisteredUser";
        }

        [TSClass]
        public static class UserRoles
        {
            [TSInitialize]
            public const string KLUser = "UserRoles.KLUser";
        }

        [TSClass]
        public static class TransactionReasons
        {
            //!!! Insert ID after ':' for every transaction
            
            [TSInitialize]
            public const string RewardClaimed = "TransactionReasons.REWARD_CLAIMED:";
            [TSInitialize]
            public const string P2PCallEnded = "TransactionReasons.P2P_CALL_ENDED:";
        }

        public static class WebClientFuncNames
        {
            public const string DisplayNotification = "displayNotification";
            public const string DisplayChatMsg = "displayChatMsg";
        }
    }
}