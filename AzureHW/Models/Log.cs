namespace AzureHW.Models
{
    /// <summary>
    /// Class contains structure for returning log entries.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Attemt result which can be success or failure.
        /// </summary>
        public string AttemptResult { get; set; }

        /// <summary>
        /// Time when date were fetched in format MM.dd.yyyy HH:mm:ss.
        /// </summary>
        public string AttemptDateTime { get; set; }
    }
}
