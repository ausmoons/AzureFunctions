using AzureHW.Validators;
using Xunit;

namespace AzureHWTests.Validators
{
    public class TimePeriodValidatorTests
    {
        [Theory]
        [InlineData("04.22.2021 10:00:00", "04.22.2021 12:00:00")]
        [InlineData("04/22/2021 12:00:00", "04/22/2021 12:00:00")]
        [InlineData("04-22-2021 12:00:00", "04-22-2021 12:00:00")]
        [InlineData("04 22 2021 12:00:00", "04 22 2021 12:00:00")]
        [InlineData("04.22.2021", "04.22.2021")]
        [InlineData("04 22 2021", "04 23 2021")]
        public void IsTimePeriodValid_ValidTimePeriod(string datefrom, string dateto)
        {
            // Arrange
            bool expectedResult = true;

            // Act
            bool result = TimePeriodValidator.IsDateFormatAndPeriodValid(datefrom, dateto);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData("40.04.2021 00:00:00", "96.04.2021 00:00:00")]
        [InlineData("fdgd", "dfgdfg")]
        [InlineData("22 00:00:00", "04")]
        [InlineData("22 04 2021 12:00:00", "22 04 2021 12:00:00")]
        [InlineData("04 25 2021 12:00:00", "04 22 2021 12:00:00")]
        [InlineData("", "04.22.2021 12:00:00")]
        [InlineData("04.22.2021 10:00:00", "")]
        public void IsTimePeriodValid_InvalidTimePeriod(string datefrom, string dateto)
        {
            // Arrange
            bool expectedResult = false;

            // Act
            bool result = TimePeriodValidator.IsDateFormatAndPeriodValid(datefrom, dateto);

            // Assert
            Assert.Equal(result, expectedResult);
        }
    }
}
