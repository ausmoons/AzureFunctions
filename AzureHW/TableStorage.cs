using AzureHW.Interfaces;
using AzureHW.Models;
using AzureHW.StorageManagers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureHW
{
    /// <summary>
    /// Class which contains all possible actions with table storage.
    /// </summary>
    public class TableStorage : ITableStorage
    {
        private readonly IMapper _mapper;
        private readonly TableStorageManager _tableStorageManager = new TableStorageManager();

        public TableStorage(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Method which adds log entry to table.
        /// </summary>
        /// <param name="statusCode">Status code of get request.</param>
        public bool AddDataToTable(string statusCode, string tableName)
        {
            LogEntity mappedData = _mapper.MapResponseToLogEntity(statusCode);

            var newEntity = new LogEntity()
            {
                PartitionKey = mappedData.AttemptDateTime,
                RowKey = mappedData.AttemptResult.ToString(),
            };

            return  _tableStorageManager.AddToTable(newEntity, tableName);          
        }

        /// <summary>
        /// Retrieves table data entries from specifi time period.
        /// </summary>
        /// <param name="datefrom">From which date entries should be picked.</param>
        /// <param name="dateto">Till which date entries should be picked.</param>
        public async Task<List<LogEntity>> GetTableDataFromPeriod(string datefrom, string dateto)
        {
            string filterBySelectedDateRange = $"PartitionKey ge '{datefrom}' and PartitionKey le '{dateto}'";
            return await _tableStorageManager.GetTableData(filterBySelectedDateRange);
        }
    }
}
