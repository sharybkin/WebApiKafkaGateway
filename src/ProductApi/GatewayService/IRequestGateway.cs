using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Data;
using Microsoft.Extensions.Primitives;

namespace ProductApi.GatewayService
{
    public interface IRequestGateway<T>
    {
        Task<IEnumerable<T>> GetAsync(CancellationToken token = default);
    }
}