using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class DataNotRetrievedFromTableStorageException : Exception
    {
        public DataNotRetrievedFromTableStorageException() : base("Can't get data from table storage.")
        {
        }
    }
}