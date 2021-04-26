namespace AzureHW.Enums
{
    /// <summary>
    /// Enums for possible data fethcing result.
    /// </summary>
    public enum AttemptValue
    {
        /// <summary>
        /// Indicates successful data fetching action, status code OK (200).
        /// </summary>
        Success = 10,

        /// <summary>
        /// Indicates not successful data fetching action.
        /// </summary>
        Failure = 20,
    }
}
