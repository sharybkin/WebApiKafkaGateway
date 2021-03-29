using Common.GatewayService;
using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Data;
using RequestHandlerService.BackgroundService;
using RequestHandlerService.Handlers;

namespace RequestHandlerService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureConsumerService(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka");
            services.Configure<KafkaConfiguration>(kafkaConfig);

            services.AddHostedService<RequestConsumerService>();
        }

        public static void ConfigureCommandHandler(this IServiceCollection services)
        {
            services.AddSingleton<IProducer, Producer>();
            services.AddSingleton<IGatewayMessageHandler, GatewayMessageHandler>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}