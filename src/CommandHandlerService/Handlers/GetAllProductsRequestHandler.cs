using System.Linq;
using System.Threading.Tasks;
using Common.Commands.ResponseCommands;
using Common.GatewayService;
using Common.Models.Gateway;
using Common.Queries;
using ProductApi.Data;

namespace CommandHandlerService.Handlers
{
    public class GetAllProductsRequestHandler
    {
        private readonly IProductRepository _repository;
        private readonly IProducer _producer;

        public GetAllProductsRequestHandler(IProductRepository repository, IProducer producer)
        {
            _repository = repository;
            _producer = producer;
        }

        public async Task Handle(GetAllProductsQuery command)
        {
            var products = _repository.GetAll().ToList();
            var responseCommand = new GetAllProductsResponseCommand(products, command.CommandId);
            var gatewayMessage = new GatewayMessage(responseCommand);

            await _producer.Produce(gatewayMessage);
        }
    }
}