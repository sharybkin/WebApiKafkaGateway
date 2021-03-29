using System.Threading;
using System.Threading.Tasks;

namespace Common.GatewayService
{
    public interface IGatewayMessageHandler
    {
        public Task Handle(string message, CancellationToken cancellationToken);
    }
}