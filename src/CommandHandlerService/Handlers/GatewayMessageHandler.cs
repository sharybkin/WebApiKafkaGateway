using System.Threading;
using System.Threading.Tasks;
using Common.Commands.RequestCommands;
using Common.GatewayService;
using Common.Models.Gateway;
using Common.Queries;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductApi.Data;

namespace CommandHandlerService.Handlers
{
    public class GatewayMessageHandler : BaseMessageHandler
    {
        private readonly ILogger<GatewayMessageHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IProducer _producer;

        public GatewayMessageHandler(ILogger<GatewayMessageHandler> logger, IProductRepository productRepository, IProducer producer): base(logger)
        {
            _logger = logger;
            _productRepository = productRepository;
            _producer = producer;
        }
        
        protected sealed override async Task ProcessMessage(GatewayMessage message, CancellationToken cancellationToken)
        {
            switch (message.CommandName)
            {
                case nameof(GetAllProductsQuery):
                {
                    var getAllProductsQuery = JsonConvert.DeserializeObject<GetAllProductsQuery>(message.Command);
                    if (getAllProductsQuery != null)
                    {
                        var requestHandler = new GetAllProductsRequestHandler(_productRepository, _producer);
                        await requestHandler.Handle(getAllProductsQuery);
                    }
                    else
                        LogIncorrectCommandBody();
                    break;
                }
                case nameof(GetProductByIdQuery):
                {
                    var getProductByIdQuery = JsonConvert.DeserializeObject<GetProductByIdQuery>(message.Command);
                    if (getProductByIdQuery != null)
                    {
                        var requestHandler = new GetProductByIdHandle(_productRepository, _producer);
                        await requestHandler.Handle(getProductByIdQuery);
                    }
                    else
                        LogIncorrectCommandBody();
                    break;
                }
                case nameof(CreateProductRequestCommand):
                {
                    var createProductRequestCommand = JsonConvert.DeserializeObject<CreateProductRequestCommand>(message.Command);
                    if (createProductRequestCommand != null)
                    {
                        var requestHandler = new CreateProductRequestHandler(_productRepository, _producer);
                        await requestHandler.Handle(createProductRequestCommand);
                    }
                    else
                        LogIncorrectCommandBody();
                    break;
                }
                default:
                    _logger.LogWarning($"Unhandled command: {message.CommandName}");
                    break;
            }
        }
    }
}