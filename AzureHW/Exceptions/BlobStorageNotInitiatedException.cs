using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class BlobStorageNotInitiatedException : Exception
    {
        public BlobStorageNotInitiatedException() : base("Blob storage was not initialized.")
        {
        }
    }
}