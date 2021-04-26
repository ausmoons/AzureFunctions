using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class CantAddDataToBlobStorageException : Exception
    {
        public CantAddDataToBlobStorageException() :base("Can't add date to blob storage.")
        {
        }
    }
}