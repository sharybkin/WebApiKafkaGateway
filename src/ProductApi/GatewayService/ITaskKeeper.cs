using System;
using System.Threading.Tasks;

namespace ProductApi.GatewayService
{
    public interface ITaskKeeper<T>
    {
        public void Keep(Guid messageId, TaskCompletionSource<object> tcs);

        public bool TryRemoveTsc(Guid messageId, out TaskCompletionSource<object> tcs);
    }
}