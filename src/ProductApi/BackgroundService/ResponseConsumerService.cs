using Common.GatewayService;
using Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ProductApi.BackgroundService
{
    public class ResponseConsumerService : ConsumerService
    {
        public ResponseConsumerService(ILogger<ResponseConsumerService> logger, IOptions<KafkaConfiguration> kafkaOptions, IGatewayMessageHandler handler) : base(logger, kafkaOptions, handler)
        {
        }
    }
}