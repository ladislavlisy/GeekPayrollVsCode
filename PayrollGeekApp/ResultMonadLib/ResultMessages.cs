namespace ResultMonad
{
    internal static class ResultMessages
    {
        public static string NoValueForFailure => "There is no value for failure.";
        public static string NoErrorForSuccess => "There is no error for success.";
        public static string SuccessResultMustHaveValue => "Success result must have a value.";
        public static string FailureResultMustHaveError => "Error result must have an error.";
    }
}