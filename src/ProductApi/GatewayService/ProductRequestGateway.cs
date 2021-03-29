using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.GatewayService;
using Common.Models.Data;
using Common.Queries;
using ProductApi.Handlers;

namespace ProductApi.GatewayService
{
    public class ProductRequestGateway : IRequestGateway<Product>
    {

        private readonly IProducer _producer;
        private readonly ITaskKeeper<Product> _taskKeeper;

        public ProductRequestGateway(IProducer producer, ITaskKeeper<Product> taskKeeper)
        {
            _producer = producer;
            _taskKeeper = taskKeeper;
        }

        public async Task<IEnumerable<Product>> GetAsync(CancellationToken token = default)
        {
            var command = new GetAllProductsQuery();
            var commandHandler = new GetAllProductsRequestHandler(_producer, _taskKeeper);
            return await commandHandler.Handle(command, token);
        }
    }
}