using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands.ResponseCommands;
using Common.Models.Data;
using MediatR;
using ProductApi.GatewayService;

namespace ProductApi.Handlers
{
    public class GetAllProductsResponseHandler : IRequestHandler<GetAllProductsResponseCommand, bool>
    {
        
        private readonly ITaskKeeper<Product> _taskKeeper;

        public GetAllProductsResponseHandler(ITaskKeeper<Product> taskKeeper)
        {
            _taskKeeper = taskKeeper;
        }

        public Task<bool> Handle(GetAllProductsResponseCommand request, CancellationToken cancellationToken)
        {
            if(_taskKeeper.TryRemoveTsc(request.CommandId, out var tcs))
                tcs.SetResult(request.Products);

            return Task.FromResult(true);
        }
    }
}