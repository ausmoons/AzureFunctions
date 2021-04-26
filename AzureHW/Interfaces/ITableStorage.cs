using AzureHW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureHW.Interfaces
{
    /// <summary>
    /// Interface of TableStorage class.
    /// </summary>
    public interface ITableStorage
    {
        /// <summary>
        /// Method which adds log entry to table.
        /// </summary>
        /// <param name="statusCode">Status code of get request.</param>
        bool AddDataToTable(string statusCode, string tableName);

        /// <summary>
        /// Retrieves table data entries from specifi time period.
        /// </summary>
        /// <param name="dateFrom">From which date entries should be picked.</param>
        /// <param name="dateTo">Till which date entries should be picked.</param>
        Task<List<LogEntity>> GetTableDataFromPeriod(string dateFrom, string dateTo);
    }
}
