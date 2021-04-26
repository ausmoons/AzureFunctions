using AzureHW.Interfaces;
using AzureHW.Models;
using AzureHWFunctions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace AzureHWTests.AzureHWFunctionsTests
{
    public class GetLogsFunctionTests
    {
        private readonly Mock<ITableStorage> _tableStorageMock;
        private readonly Mock<IMapper> _mappingsMock;
        private readonly GetLogsFunction _getLogsFunction;

        public GetLogsFunctionTests()
        {
            _tableStorageMock = new Mock<ITableStorage>();
            _mappingsMock = new Mock<IMapper>();

            _getLogsFunction = new GetLogsFunction(_tableStorageMock.Object, _mappingsMock.Object);

        }

        [Fact]
        public async Task GetLogsFromTimePeriod_LogsRetrieved()
        {
            // Arrange
            var reqMock = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();
            dictionary.Add("datefrom", new StringValues("04.23.2021"));
            dictionary.Add("dateto", new StringValues("04.25.2021"));
            QueryCollection query = new QueryCollection(dictionary);

            reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));

            var ctor = typeof(TableQuerySegment<LogEntity>)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(c => c.GetParameters().Count() == 1);

            var entityList = ctor.Invoke(
                 new object[] { new List<LogEntity>()
                {
                    new LogEntity{ PartitionKey = "04.24.2021 17:40:04", RowKey = "Success" },
                    new LogEntity{ PartitionKey = "04.24.2022 17:40:04", RowKey = "Failure" },
                }}) as List<LogEntity>;

            _tableStorageMock.Setup(
                x => x.GetTableDataFromPeriod(It.IsAny<string>(), It.IsAny<string>())).Returns(() => Task.FromResult(entityList));

            // Act
            var result = await _getLogsFunction.GetLogsFromTimePeriod(reqMock.Object);
            var okResult = result as OkObjectResult;


            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
