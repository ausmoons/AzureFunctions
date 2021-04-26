using System;

namespace AzureHW.Validators
{
    public class TimePeriodValidator
    {
        /// <summary>
        /// Validates user input for data time period.
        /// </summary>
        /// <param name="datefrom">Earliest date from which data should be collected.</param>
        /// <param name="dateto">Latestt date till which data should be collected.</param>
        /// <returns></returns>
        public static bool IsDateFormatAndPeriodValid(string datefrom, string dateto)
        {
            var tryParseDateFrom = DateTime.TryParse(datefrom, out var dateFormatFrom);
            var tryParseDateTo = DateTime.TryParse(dateto, out var dateFormatTo);

            if (tryParseDateFrom && tryParseDateTo)
            {
                if (dateFormatFrom > dateFormatTo)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
