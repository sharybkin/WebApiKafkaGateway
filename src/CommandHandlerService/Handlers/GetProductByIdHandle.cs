using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands.ResponseCommands;
using Common.GatewayService;
using Common.Models.Gateway;
using Common.Queries;
using ProductApi.Data;

namespace RequestHandlerService.Handlers
{
    public class GetProductByIdHandle
    {
        
        private readonly IProductRepository _repository;
        private readonly IProducer _producer;

        public GetProductByIdHandle(IProductRepository repository, IProducer producer)
        {
            _repository = repository;
            _producer = producer;
        }

        public async Task Handle(GetProductByIdQuery command)
        {
            var product = await _repository.GetProductByIdAsync(command.Id);
            var responseCommand = new GetProductByIdResponseCommand(product, command.CommandId);
            var gatewayMessage = new GatewayMessage(responseCommand);

            await _producer.Produce(gatewayMessage);
        }
    }
}