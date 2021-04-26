using AzureHW.Enums;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureHW.Models
{
    /// <summary>
    /// Class contains structure for returning log entity.
    /// </summary>
    public class LogEntity: TableEntity
    {
        /// <summary>
        /// Attemt result which can be success or failure enum.
        /// </summary>
        public AttemptValue AttemptResult { get; set; }

        /// <summary>
        /// Time when date were fetched in format MM.dd.yyyy HH:mm:ss.
        /// </summary>
        public string AttemptDateTime { get; set; }
    }
}
