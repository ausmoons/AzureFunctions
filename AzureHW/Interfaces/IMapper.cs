using AzureHW.Models;
using System.Collections.Generic;

namespace AzureHW.Interfaces
{
    /// <summary>
    /// Interface for Mappings class.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps log data to Log entity.
        /// </summary>
        /// <param name="statusCode">Status code of get request.</param>
        LogEntity MapResponseToLogEntity(string statusCode);

        /// <summary>
        /// Maps returned data to log entity.
        /// </summary>
        /// <param name="returnedData">Returned data from table storage.</param>
        List<Log> MapReturnedDataToLog(List<LogEntity> returnedData);
    }
}