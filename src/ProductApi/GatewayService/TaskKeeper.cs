using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ProductApi.Exceptions;

namespace ProductApi.GatewayService
{
    public class TaskKeeper<T> : ITaskKeeper<T> where T: class
    {
        private readonly ConcurrentDictionary<Guid,TaskCompletionSource<object>> _dictionary = new ConcurrentDictionary<Guid, TaskCompletionSource<object>>();

        private const int MessageLifeTimeMs = 3000;

        public void Keep(Guid messageId, TaskCompletionSource<object> tcs)
        {
            if(!_dictionary.TryAdd(messageId,tcs))
                throw new TaskKeeperException("Message already exists");
            
            Task.Run(async () =>
            {
                await Task.Delay(MessageLifeTimeMs);
                if(TryRemoveTsc(messageId, out var taskCompletionSource))
                    taskCompletionSource.SetException(new TimeoutException("The response from kafka was not received in the allotted time"));
            });
        }

        public bool TryRemoveTsc(Guid messageId, out TaskCompletionSource<object> tcs)
        {
            return _dictionary.TryRemove(messageId, out tcs);
        }
    }
}