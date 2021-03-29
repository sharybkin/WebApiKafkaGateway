using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.GatewayService;
using Common.Models.Data;
using Common.Models.Gateway;
using Common.Queries;
using MediatR;
using ProductApi.Exceptions;
using ProductApi.GatewayService;

namespace ProductApi.Handlers
{
    public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        
        private readonly ITaskKeeper<Product> _taskKeeper;
        private readonly IProducer _producer;

        public GetProductByIdRequestHandler(ITaskKeeper<Product> taskKeeper, IProducer producer)
        {
            _taskKeeper = taskKeeper;
            _producer = producer;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var message = new GatewayMessage(request);
            var tcs = new TaskCompletionSource<object>();

            _taskKeeper.Keep(request.CommandId,tcs); 
            await _producer.Produce(message);
            
            var result = await tcs.Task;

            if (result is Product product)
                return product;
            else
                throw new GatewayException("Incorrect response format");
        }
    }
}