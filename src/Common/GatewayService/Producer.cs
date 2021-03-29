using System;
using System.Threading.Tasks;
using Common.Models.Gateway;
using Common.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Common.GatewayService
{
    public class Producer : IProducer
    {
        private ProducerConfig _config;
        private string _topic;
        private readonly ILogger<Producer> _logger;
        
        public Producer(IOptions<KafkaConfiguration> kafkaOptions, ILogger<Producer> logger)
        {
            _logger = logger;
            ConfigureProducer(kafkaOptions);
        }

        private void ConfigureProducer(IOptions<KafkaConfiguration> kafkaOptions)
        {
            _topic = kafkaOptions.Value.OutgoingTopic;
            _config = new ProducerConfig
            {
                BootstrapServers = kafkaOptions.Value.BootstrapServers,
            };
        }

        public async Task Produce(GatewayMessage message)
        {
            
            using var producer = new ProducerBuilder<Null, string>(_config)
                .SetErrorHandler((_, e) =>
                {
                    _logger.LogError($"Kafka Error {e.Code}: {e.Reason}");
                })
                .Build();
            
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(message);

                var dr = await producer.ProduceAsync(
                    topic: _topic, 
                    message: new Message<Null, string> { Value = jsonMessage }
                );
                    
                _logger.LogInformation($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}' with status {dr.Status.ToString()}");
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Delivery failed: {e.Error.Reason}");
            }
            catch (Exception ex) {
                _logger.LogError($"Delivery failed: {ex.Message}");
            }
        }
    }
}