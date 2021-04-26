using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class InvalidStatusCodeFormatException : Exception
    {
        public InvalidStatusCodeFormatException() : base("Data fetching attempt status code was not correct.")
        {
        }
    }
}