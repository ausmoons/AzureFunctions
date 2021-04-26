using System.Threading.Tasks;

namespace AzureHW.Interfaces
{
    /// <summary>
    /// Interface of BlobStorage class.
    /// </summary>
    public interface IBlobStorage
    {
        /// <summary>
        /// Method which adds data to blob storage.
        /// </summary>
        /// <param name="dataToSave">Data which needs to be saved in blob</param>
        bool AddDataToBlob(string dataToSave);

        /// <summary>
        /// Method which returns data from blob storage.
        /// </summary>
        /// <param name="blobName">Blob name by which appropriate data is retrieved.</param>
        Task<string> GetBlobData(string blobName);
    }
}