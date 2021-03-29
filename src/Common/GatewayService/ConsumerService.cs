using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.GatewayService
{
    public class ConsumerService : IHostedService
    {
        private readonly ILogger _logger;
        private CancellationTokenSource _cts;
        private ConsumerConfig _config;
        private string _topic;
        private readonly IGatewayMessageHandler _handler;

        public ConsumerService(ILogger logger, IOptions<KafkaConfiguration> kafkaOptions, IGatewayMessageHandler handler)
        {
            _logger = logger;
            _handler = handler;
            ConfigureConsumer(kafkaOptions);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = new CancellationTokenSource();
            Task.Run(StartConsume, _cts.Token);
            _logger.LogInformation($"{this.GetType().Name} started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            _logger.LogWarning($"{this.GetType().Name} has been stopped.");
            return Task.CompletedTask;
        }

        private void ConfigureConsumer(IOptions<KafkaConfiguration> kafkaOptions)
        {
            _config = new ConsumerConfig
            { 
                GroupId = kafkaOptions.Value.GroupId,
                BootstrapServers = kafkaOptions.Value.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Latest,
            };

            _topic = kafkaOptions.Value.IncomingTopic;
        }

        private async void StartConsume()
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            consumer.Subscribe(_topic);
            
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(_cts.Token);
                    var message = consumeResult.Message.Value;
                    _logger.LogInformation($"Consumed message '{message}' at: '{consumeResult.TopicPartitionOffset}'.");
                    await _handler.Handle(message,_cts.Token);
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Error occured: {e.Error.Reason}");
                }
                catch (OperationCanceledException) 
                {
                    consumer.Close();
                    break;
                }
                catch (Exception ex) 
                {
                    _logger.LogError($"Unhandled Exception: {ex.Message}");
                    consumer.Close();
                    break;
                }
            }
        } 
        
    }
}