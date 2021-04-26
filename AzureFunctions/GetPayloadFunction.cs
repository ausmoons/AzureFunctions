using AzureHW.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;

namespace AzureHWFunctions
{
    /// <summary>
    /// Class which is executed when http://localhost:7071/api/GetPayloadFunction is triggered.
    /// </summary>
    public class GetPayloadFunction
    {
        private IBlobStorage _blobStorage;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataFetchingFunction));

        public GetPayloadFunction(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }

        /// <summary>
        /// Function which returns payload from blob storage byspecified blob name.
        /// </summary>
        /// <param name="req">Request parameters.</param>
        [FunctionName("GetPayloadFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            string blobName = req.Query["blobName"];
            _log.Info($"Trying to retrieve blob data by blob name {blobName}");
            var returnedData = await _blobStorage.GetBlobData(blobName);
            _log.Info($"Data was retrieved - {!string.IsNullOrEmpty(returnedData)}");
            return new OkObjectResult(returnedData);
        }
    }
}
