using AzureHW.Exceptions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Threading.Tasks;

namespace AzureHW.StorageManagers
{
    /// <summary>
    /// Blob storage manager class
    /// </summary>
    public class BlobStorageManager
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(BlobStorageManager));
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _blobClient;
        private CloudBlobContainer _container;

        /// <summary>
        /// Initializes a new instance of the BlobStorageManager class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string in local.settings.json.</param>
        public BlobStorageManager()
        {
            _storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            _blobClient = _storageAccount.CreateCloudBlobClient();
            _blobClient.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(2), 4);
        }

        /// <summary>
        /// Updates or created a blob in Azure blob storage.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="blobName">Name of the blob.</param>
        /// <param name="content">The content of the blob.</param>
        /// <returns>True if data was added successfully.</returns>
        public bool AddToBlob(string containerName, string blobName, string dataToSave)
        {
            if(!this.DoesContainerExist(containerName))
            {
                this.CreateContainer(containerName);
                _log.Info($"Created container {containerName}");
            }

            bool isValid = false;

            try
            {
                CloudBlockBlob blob = _container.GetBlockBlobReference(blobName);
                Task result = blob.UploadTextAsync(dataToSave);
                result.Wait();
                isValid = result.Status == TaskStatus.RanToCompletion;
            }
            catch(Exception e)
            {
                _log.Error($"Data to blob was not added", e);
                throw new CantAddDataToBlobStorageException();
            }

            _log.Info($"Data to blob was added - {isValid}");
            return isValid;
        }

        /// <summary>
        /// Method which returns data from blob storage.
        /// </summary>
        /// <param name="blobName">Blob name by which appropriate data is retrieved.</param>
        public async Task<string> GetBlobData(string blobName)
        {
            CloudBlockBlob blob = _container.GetBlockBlobReference(blobName);

            try
            {
                return await blob.DownloadTextAsync();
            }
            catch(Exception e)
            {
                _log.Error($"Data from blob was not retrieved", e);
                throw new BlobNotFoundException();
            }
        }

        /// <summary>
        /// Checks if a container exist.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>True if conainer exists.</returns>
        private bool DoesContainerExist(string containerName)
        {
            _container = _blobClient.GetContainerReference(containerName);

            return _container != null;
        }

        /// <summary>
        /// Creates the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>True if contianer was created successfully.</returns>
        private bool CreateContainer(string containerName)
        {
            bool isValid = false;

            try
            {
                Task result = _container.CreateAsync();
                result.Wait();
                isValid = result.Status == TaskStatus.RanToCompletion;
            }
            catch (Exception e)
            {
                _log.Error($"Blob container was not created", e);
                throw new BlobContainerNotCreatedException();
            }

            return isValid;
        }
    }  
}
