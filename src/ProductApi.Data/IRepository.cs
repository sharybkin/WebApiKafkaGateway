using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);
    }
}