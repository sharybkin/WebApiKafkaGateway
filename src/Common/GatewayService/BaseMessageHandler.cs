using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Gateway;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.GatewayService
{
    public abstract class BaseMessageHandler : Common.GatewayService.IGatewayMessageHandler
    {
        private readonly ILogger _logger;

        protected BaseMessageHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(string message, CancellationToken cancellationToken)
        {
            try
            {
                var gatewayMessage = JsonConvert.DeserializeObject<GatewayMessage>(message);
                if (gatewayMessage != null)
                {
                    _logger.LogInformation($"Received command: {gatewayMessage.CommandName}");
                    await ProcessMessage(gatewayMessage, cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occured: {e.Message}");
            }
        }

        protected abstract Task ProcessMessage(GatewayMessage message, CancellationToken cancellationToken);
        
        protected virtual void LogIncorrectCommandBody() => _logger.LogError("Incorrect command body");
    }
}