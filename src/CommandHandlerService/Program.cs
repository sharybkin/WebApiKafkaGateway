using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandHandlerService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommandHandlerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, confBuilder) =>
                {
                    confBuilder.AddEnvironmentVariables();
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    Console.WriteLine(environment);
                    
                    confBuilder.AddJsonFile($"appsettings.{environment}.json", true, false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureConsumerService(hostContext.Configuration); 
                    services.ConfigureCommandHandler();
                });
    }
}