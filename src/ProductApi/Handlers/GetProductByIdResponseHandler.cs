using System.Threading;
using System.Threading.Tasks;
using Common.Commands.ResponseCommands;
using Common.Models.Data;
using MediatR;
using ProductApi.GatewayService;

namespace ProductApi.Handlers
{
    public class GetProductByIdResponseHandler : IRequestHandler<GetProductByIdResponseCommand, bool>
    {
        private readonly ITaskKeeper<Product> _taskKeeper;

        public GetProductByIdResponseHandler(ITaskKeeper<Product> taskKeeper)
        {
            _taskKeeper = taskKeeper;
        }

        public Task<bool> Handle(GetProductByIdResponseCommand request, CancellationToken cancellationToken)
        {
            if(_taskKeeper.TryRemoveTsc(request.CommandId, out var tcs))
                tcs.SetResult(request.Product);

            return Task.FromResult(true);
        }
    }
}