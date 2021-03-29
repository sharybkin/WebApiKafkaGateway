using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands.RequestCommands;
using Common.GatewayService;
using Common.Models.Data;
using Common.Models.Gateway;
using MediatR;
using ProductApi.Exceptions;
using ProductApi.GatewayService;

namespace ProductApi.Handlers
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequestCommand, Product>
    {
        
        private readonly IProducer _producer;
        private readonly ITaskKeeper<Product> _taskKeeper;

        public CreateProductRequestHandler(IProducer producer, ITaskKeeper<Product> taskKeeper)
        {
            _producer = producer;
            _taskKeeper = taskKeeper;
        }

        public async Task<Product> Handle(CreateProductRequestCommand request, CancellationToken cancellationToken)
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