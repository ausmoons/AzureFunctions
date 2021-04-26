using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class CantAddDataToTableException : Exception
    {
        public CantAddDataToTableException() : base("Can't add data to table storage.")
        {
        }
    }
}