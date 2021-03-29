using Common.GatewayService;
using Common.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RequestHandlerService.BackgroundService
{
    public class RequestConsumerService : ConsumerService
    {
        public RequestConsumerService(ILogger<RequestConsumerService> logger, IOptions<KafkaConfiguration> kafkaOptions, IGatewayMessageHandler handler) : base(logger, kafkaOptions, handler)
        {
        }
    }
}