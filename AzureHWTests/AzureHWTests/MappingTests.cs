using AzureHW;
using AzureHW.Enums;
using AzureHW.Exceptions;
using AzureHW.Models;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace AzureHWTests.AzureHWTests
{
    public class MappingTests
    {
        [Fact]
        public void MapResponseToLogEntity_ValidStatusCode_DataMapped()
        {
            // Arrange
            var statusCode = HttpStatusCode.OK.ToString();
            var mapping = new Mapper();

            // Act
           var result = mapping.MapResponseToLogEntity(statusCode);


            // Assert
            Assert.True(result.AttemptResult == AttemptValue.Success);
            Assert.True(result.AttemptDateTime == DateTime.Now.ToString("MM.dd.yyyy HH:mm:ss"));
        }

        [Fact]
        public void MapResponseToLogEntity_InvalidStatusCode_DataIsNotMapped()
        {
            // Arrange
            var statusCode = "some string";
            var mapping = new Mapper();

            // Act & Assert
            Assert.Throws<InvalidStatusCodeFormatException>(() => mapping.MapResponseToLogEntity(statusCode));
        }

        [Fact]
        public void MapReturnedDataToLog_ValidInput_DataMapped()
        {
            // Arrange
            var entityList = new List<LogEntity>()
            {
                new LogEntity{AttemptDateTime = "04.24.2021 17:40:04" , AttemptResult = AttemptValue.Success },
                new LogEntity{ AttemptDateTime = "04.24.2022 17:40:04" , AttemptResult = AttemptValue.Failure }
            };

            var expectedResult = new List<Log>()
            {
                new Log{AttemptDateTime = "04.24.2021 17:40:04" , AttemptResult = "Success" },
                new Log{ AttemptDateTime = "04.24.2022 17:40:04" , AttemptResult = "Failure" }
            };

            var mapping = new Mapper();

            // Act
            var result = mapping.MapReturnedDataToLog(entityList);


            // Assert
            result.Equals(expectedResult);
        }
    }
}
