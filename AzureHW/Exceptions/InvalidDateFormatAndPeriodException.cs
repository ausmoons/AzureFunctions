using System;

namespace AzureHW.Exceptions
{
    [Serializable]
    public class InvalidDateFormatAndPeriodException : Exception
    {
        public InvalidDateFormatAndPeriodException() :base("Date format was incorrect. Date should be in MM.dd.yyyy HH:mm:ss foramt.")
        {
        }
    }
}