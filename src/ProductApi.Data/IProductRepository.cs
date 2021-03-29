using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Data;

namespace ProductApi.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIdAsync(Guid id);
    }
}