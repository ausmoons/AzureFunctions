using AzureHW.Interfaces;
using AzureHW.StorageManagers;
using System;
using System.Threading.Tasks;

namespace AzureHW
{
    /// <summary>
    /// Class which contains all possible actions with blobs.
    /// </summary>
    public class BlobStorage: IBlobStorage
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(BlobStorage));
        private readonly BlobStorageManager _blobStorageManager = new BlobStorageManager();

        /// <summary>
        /// Method which adds data to blob storage.
        /// </summary>
        /// <param name="dataToSave">Data which needs to be saved in blob.</param>
        /// <returns>True if data was added successfully.</returns>
        public bool AddDataToBlob(string dataToSave)
        {
            string containerName = "contentcontainer";
            string blobName = DateTime.Now.ToString("MM.dd.yyyy HH:mm:ss");
            return  _blobStorageManager.AddToBlob(containerName, blobName, dataToSave);
        }

        /// <summary>
        /// Method which returns data from blob storage.
        /// </summary>
        /// <param name="blobName">Blob name by which appropriate data is retrieved.</param>
        public async Task<string> GetBlobData(string blobName)
        {
            return await _blobStorageManager.GetBlobData(blobName);
        }
    }
}
