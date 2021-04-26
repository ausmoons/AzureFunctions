using AzureHW;
using AzureHW.Interfaces;
using log4net;
using log4net.Config;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Startup))]
[assembly: XmlConfigurator(ConfigFile = "log4net.config")]
namespace AzureHW
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            AddLogging();

            builder.Services.AddSingleton<ITableStorage, TableStorage>();
            builder.Services.AddSingleton<IBlobStorage, BlobStorage>();
            builder.Services.AddSingleton<IMapper, Mapper>();
        }

        private static void AddLogging()
        {
            log4net.Repository.ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
    }
}
