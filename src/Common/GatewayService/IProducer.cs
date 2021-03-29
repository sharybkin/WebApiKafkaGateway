using System.Threading.Tasks;
using Common.Models.Gateway;

namespace Common.GatewayService
{
    public interface IProducer
    {
        Task Produce(GatewayMessage message);
    }
}