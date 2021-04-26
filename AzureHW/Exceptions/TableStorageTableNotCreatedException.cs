using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class TableStorageTableNotCreatedException : Exception
    {
        public TableStorageTableNotCreatedException() : base("Table storage table was not created.")
        {
        }
    }
}