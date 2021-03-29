using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands.ResponseCommands;
using Common.GatewayService;
using Common.Models.Data;
using Common.Models.Gateway;
using Common.Queries;
using MediatR;
using ProductApi.Exceptions;
using ProductApi.GatewayService;

namespace ProductApi.Handlers
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProducer _producer;
        private readonly ITaskKeeper<Product> _taskKeeper;

        public GetAllProductsRequestHandler(IProducer producer, ITaskKeeper<Product> taskKeeper)
        {
            _producer = producer;
            _taskKeeper = taskKeeper;
        }
        

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var message = new GatewayMessage(request);
            var tcs = new TaskCompletionSource<object>();

            _taskKeeper.Keep(request.CommandId,tcs); 
            await _producer.Produce(message);
            
            var result = await tcs.Task;

            if (result is List<Product> products)
                return products;
            else
                throw new GatewayException("Incorrect response format");
        }
    }
}