using System.Threading.Tasks;
using Common.Commands.RequestCommands;
using Common.Commands.ResponseCommands;
using Common.GatewayService;
using Common.Models.Data;
using Common.Models.Gateway;
using ProductApi.Data;

namespace CommandHandlerService.Handlers
{
    public class CreateProductRequestHandler
    {
        private readonly IProductRepository _repository;
        private readonly IProducer _producer;

        public CreateProductRequestHandler(IProductRepository repository, IProducer producer)
        {
            _repository = repository;
            _producer = producer;
        }

        public async Task Handle(CreateProductRequestCommand command)
        {
            var product = await _repository.AddAsync(new Product
            {
                Name = command.Name,
                Price = command.Price,
                ExpirationDate = command.ExpirationDate
            });
            var responseCommand = new CreateProductResponseCommand(product, command.CommandId);
            var gatewayMessage = new GatewayMessage(responseCommand);

            await _producer.Produce(gatewayMessage);
        }
    }
}