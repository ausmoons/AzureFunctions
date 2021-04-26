using AzureHW.Interfaces;
using Microsoft.Azure.WebJobs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureHWFunctions
{
    /// <summary>
    /// Class which contains method for data fetching.
    /// </summary>
    public class DataFetchingFunction
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataFetchingFunction));
        private static HttpClient _client;
        private readonly ITableStorage _tableStorage;
        private readonly IBlobStorage _blobStorage;

        public DataFetchingFunction(HttpClient client, ITableStorage tableStorage, IBlobStorage blobStorage)
        {
            _client = client;
            _tableStorage = tableStorage;
            _blobStorage = blobStorage;
        }

        /// <summary>
        /// Fetch data from url every minute.
        /// </summary>
        /// <param name="myTimer"></param>
        [FunctionName("DataFetchingFunction")]
        public async Task FetchDataEveryMinute([TimerTrigger("%DataFetchingSchedule%")] TimerInfo myTimer)
        {
            var fetchedData = await _client.GetAsync(Environment.GetEnvironmentVariable("DataFetchingUrl"));
            var fetchedContent = await fetchedData.Content.ReadAsStringAsync();
            _log.Info($"Pulled data from {Environment.GetEnvironmentVariable("DataFetchingUrl")}");
            _tableStorage.AddDataToTable(fetchedData.StatusCode.ToString(), "AttemptLog"); 
            _blobStorage.AddDataToBlob(Convert.ToString(fetchedData) + fetchedContent);
        }
    }
}
