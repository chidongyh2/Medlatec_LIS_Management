namespace Medlatec.Infrastructure.Constants
{
    public static class ErrorMessage
    {
        public const string NotHavePermission = "You do not have permission to do this action.";
        public const string SomethingWentWrong = "Something went wrong. Please contact with administrator.";
        public const string NothingChanges = "Nothing changes. Please try again.";
        public const string NothingToShow = "Nothing to show.";
        public const string AlreadyUpdatedBy = "{0} already updated by {1}.";
        public const string AlreadyUpdatedByAnother = "Already updated by another.";
        public const string AlreadyExists = "{0} already exists.";
        public const string PleaseSelectStartDate = "Please select start date.";
        public const string PleaseSelectEndDate = "Please select end date.";
        public const string AlreadyUsedBy = "{0} already used by {1}.";
        public const string Sorry = "Sorry";
        public static string NotExists = "{0} does not exists.";
        public static string NotFound = "{0} not found.";
        public static string CanBeNotNull = "{0} can be not null.";
        public static string MustBeNumber = "{0} must be number";
    }

    public static class SuccessMessage
    {
        public const string AddSuccessful = "Add new {0} successful.";
        public const string UpdateSuccessful = "Update {0} successful.";
        public const string DeleteSuccessful = "Delete {0} successful.";
        public static string SyncDataSuccessful = "Sync data successful.";
    }
}
