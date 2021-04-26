using AzureHW.Enums;
using AzureHW.Exceptions;
using AzureHW.Interfaces;
using AzureHW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AzureHW
{
    /// <summary>
    /// Class for all mapping methods.
    /// </summary>
    public class Mapper : IMapper
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Mapper));

        /// <summary>
        /// Maps log data to Log entity.
        /// </summary>
        /// <param name="statusCode">Status code of get request.</param>
        public LogEntity MapResponseToLogEntity(string statusCode)
        {

            if (!Enum.TryParse(statusCode, out HttpStatusCode statusCodeEnum))
            {
                _log.Error($"It was not possible to convert status code - {statusCodeEnum}");
                throw new InvalidStatusCodeFormatException();
            }

            return new LogEntity()
            {
                AttemptDateTime = DateTime.Now.ToString("MM.dd.yyyy HH:mm:ss"),
                AttemptResult = statusCode == HttpStatusCode.OK.ToString() ? AttemptValue.Success : AttemptValue.Failure,
            };
        }

        /// <summary>
        /// Maps returned data to log entity.
        /// </summary>
        /// <param name="returnedData">Returned data from table storage.</param>
        public List<Log> MapReturnedDataToLog(List<LogEntity> returnedDataList)
        {
            return returnedDataList.Select(
                returnedDate => new Log() 
                {
                    AttemptDateTime = returnedDate.PartitionKey, 
                    AttemptResult = Enum.GetName(typeof(AttemptValue), returnedDate.AttemptResult),
                }).ToList();
        }
    }
}
