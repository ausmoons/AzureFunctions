using System;

namespace AzureHW
{
    [Serializable]
    public class BlobNotFoundException : Exception
    {
        public BlobNotFoundException() : base("Can't get data from blob storage. Blob name should be in format MM.dd.yyyy HH:mm:ss")
        {
        }
    }
}