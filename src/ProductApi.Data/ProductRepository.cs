using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Data;

namespace ProductApi.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = GenerateProducts().ToList();
        }
        
        public Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = GetAll().FirstOrDefault(x => x.Id == id);
            return Task.FromResult(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Task<Product> AddAsync(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            
            entity.Id = Guid.NewGuid();
            _products.Add(entity);

            return Task.FromResult(entity);
        }
        
        public async Task<Product> UpdateAsync(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                var product = await GetProductByIdAsync(entity.Id);

                product.Name = entity.Name;
                product.Price = entity.Price;
                product.ExpirationDate = entity.ExpirationDate;
               
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }

        private static IEnumerable<Product> GenerateProducts()
        {
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                yield return new Product {
                    Id = Guid.NewGuid(),
                    Name = "Candy", 
                    ExpirationDate = DateTimeOffset.Now.Add(TimeSpan.FromDays(180)),
                    Price = new Price
                        {
                            Amount = random.Next(10,100),
                            Currency = new Currency{Code = "USD", Number = "840"}
                        }
                    };    
            }
        }
    }
}