using System;

namespace AzureHW.Exceptions
{
    public class BlobContainerNotCreatedException : Exception
    {
        public BlobContainerNotCreatedException() : base("Blob container was not created.")
        {
        }
    }
}
