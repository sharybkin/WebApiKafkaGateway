using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands.ResponseCommands;
using Common.GatewayService;
using Common.Models.Data;
using Common.Models.Gateway;
using Common.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductApi.GatewayService;

namespace ProductApi.Handlers
{
    public class GatewayMessageHandler : BaseMessageHandler
    {
        private readonly ILogger<GatewayMessageHandler> _logger;
        private readonly IMediator _mediator;

        public GatewayMessageHandler(ILogger<GatewayMessageHandler> logger, IMediator mediator) : base(logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected sealed override async Task ProcessMessage(GatewayMessage message, CancellationToken cancellationToken)
        {
            try
            {
                switch (message.CommandName)
                {
                    case nameof(GetAllProductsResponseCommand):
                    {
                        var getAllProductsCommand = JsonConvert.DeserializeObject<GetAllProductsResponseCommand>(message.Command);
                        if(getAllProductsCommand != null)
                            await _mediator.Send(getAllProductsCommand, cancellationToken);
                        else
                            LogIncorrectCommandBody();
                        
                        /* TODO: ---
                        var requestHandler = new GetAllProductsResponseHandler(_productTaskKeeper);
                        requestHandler.Handle(command, CancellationToken.None);
                        */
                        break;
                    }
                    case nameof(GetProductByIdResponseCommand):
                        var getProductByIdCommand = JsonConvert.DeserializeObject<GetProductByIdResponseCommand>(message.Command);
                        if(getProductByIdCommand != null)
                            await _mediator.Send(getProductByIdCommand, cancellationToken);
                        else
                            LogIncorrectCommandBody();
                        break;
                    case nameof(CreateProductResponseCommand):
                        var createProductResponseCommand = JsonConvert.DeserializeObject<CreateProductResponseCommand>(message.Command);
                        if(createProductResponseCommand != null)
                            await _mediator.Send(createProductResponseCommand, cancellationToken);
                        else
                            LogIncorrectCommandBody();
                        break;
                    default:
                        _logger.LogWarning($"Unhandled command: {message.CommandName}");
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unhandled {nameof(ProcessMessage)} error");
            }
        }
    }
}