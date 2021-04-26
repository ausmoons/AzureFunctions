using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class TableStorageNotInitiatedException : Exception
    {
        public TableStorageNotInitiatedException() : base("Can't initialize table storage.")
        {
        }
    }
}