using Common.GatewayService;
using Common.Models.Data;
using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.BackgroundService;
using ProductApi.GatewayService;
using ProductApi.Handlers;

namespace ProductApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureGatewayService(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka");
            services.Configure<KafkaConfiguration>(kafkaConfig);

            services.AddSingleton<IProducer, Producer>();
            services.AddSingleton<IRequestGateway<Product>, ProductRequestGateway>();
            
            services.AddSingleton(typeof(ITaskKeeper<>), typeof(TaskKeeper<>));

            services.AddSingleton<IGatewayMessageHandler, GatewayMessageHandler>();
            services.AddHostedService<ResponseConsumerService>();
        }
    }
}