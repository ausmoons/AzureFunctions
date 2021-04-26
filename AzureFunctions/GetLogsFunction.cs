using AzureHW.Exceptions;
using AzureHW.Validators;
using AzureHW.Interfaces;
using AzureHW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureHWFunctions
{
    /// <summary>
    /// Class which is executed when http://localhost:7071/api/GetLogsFunction is triggered.
    /// </summary>
    public class GetLogsFunction
    {
        private readonly ITableStorage _tableStorage;
        private readonly IMapper _mappings;
        private string _dateFrom = "";
        private string _dateTo = "";
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataFetchingFunction));

        public GetLogsFunction(ITableStorage tableStorage, IMapper mappings)
        {
            _tableStorage = tableStorage;
            _mappings = mappings;
        }

        /// <summary>
        /// Function which returns logs from table storage from specific time period.
        /// </summary>
        /// <param name="req">Request parameters.</param>
        [FunctionName("GetLogsFunction")]
        public async Task<IActionResult> GetLogsFromTimePeriod(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {           
            _dateFrom = req.Query["datefrom"];
            _dateTo = req.Query["dateto"];

            if (!TimePeriodValidator.IsDateFormatAndPeriodValid(_dateFrom, _dateTo))
            {
                _log.Error($"Date from {_dateFrom} and or date to {_dateTo} was not not in correct format and" +
                    $" it was not possible to finish the action. Expected date format MM.dd.yyyy HH:mm:ss.");
                throw new InvalidDateFormatAndPeriodException();
            }
           
            List<LogEntity> returnedData = await _tableStorage.GetTableDataFromPeriod(_dateFrom, _dateTo);
            var mappedData = _mappings.MapReturnedDataToLog(returnedData);

            return new OkObjectResult(mappedData);
        }
    }
}
