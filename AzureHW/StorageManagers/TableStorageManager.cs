using AzureHW.Exceptions;
using AzureHW.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureHW.StorageManagers
{
    /// <summary>
    /// Blob storage manager class
    /// </summary>
    public class TableStorageManager
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(TableStorageManager));
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;
        private CloudTable _table;

        /// <summary>
        /// Initializes a new instance of the BlobStorageManager class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string in local.settings.json.</param>
        public TableStorageManager()
        {
            _storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        /// <summary>
        /// Updates or created a blob in Azure blob storage
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="blobName">Name of the blob.</param>
        /// <param name="content">The content of the blob.</param>
        /// <returns>True if data was added successfully.</returns>
        public bool AddToTable(LogEntity dataToSave, string tableName)
        {
            if(!this.DoesTableExist(tableName))
            {              
                this.CreateTableIfNotExist();
                _log.Info($"Created table storage table {tableName}");
            }

            bool isValid = false;
            try
            {
                TableOperation dataToTable = TableOperation.Insert(dataToSave);
                Task result = _table.ExecuteAsync(dataToTable);
                result.Wait();
                isValid = result.Status == TaskStatus.RanToCompletion;
            }
            catch(Exception e)
            {
                _log.Error($"Data to table was not added", e);
                throw new CantAddDataToTableException();
            }

            _log.Info($"Data to table was added - {isValid}");
            return isValid;
        }

        /// <summary>
        /// Method which returns data from blob storage.
        /// </summary>
        /// <param name="blobName">Blob name by which appropriate data is retrieved.</param>
        public async Task<List<LogEntity>> GetTableData(string filterBySelectedDateRange)
        {
            TableQuery<LogEntity> query = new TableQuery<LogEntity>().Where(filterBySelectedDateRange);
            _log.Info($"Query by which data was retrieved - {filterBySelectedDateRange}.");
            var retrievedData = new List<LogEntity>();
            TableContinuationToken tableContinuationToken = null;

            // There is a loo to retrieve all needed data not only part of it.
            try
            {
                do
                {
                    TableQuerySegment<LogEntity> queryResponse = await _table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                    tableContinuationToken = queryResponse.ContinuationToken;
                    retrievedData.AddRange(queryResponse?.Results);
                }
                while (tableContinuationToken != null);

                _log.Info($"Amount of retrieved data - {retrievedData.Count}");
                return retrievedData;
            }
            catch (Exception e)
            {
                _log.Error($"Data from table was not retrieved", e);
                throw new DataNotRetrievedFromTableStorageException();
            }
        }

        /// <summary>
        /// Checks if a container exist.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>True if conainer exists.</returns>
        private bool DoesTableExist(string tableName)
        {
            _table = _tableClient.GetTableReference(tableName);
            _log.Info($"Table exist - {_table}.");

            return _table != null;
        }

        /// <summary>
        /// Creates the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>True if contianer was created successfully.</returns>
        private void CreateTableIfNotExist()
        {
            try
            {
                Task result = _table.CreateIfNotExistsAsync();
            }
            catch(Exception e) 
            {
                _log.Error($"Table was not created", e);
                throw new TableStorageTableNotCreatedException();
            }
        }
    }  
}
