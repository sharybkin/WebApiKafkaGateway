using System;
using Common.Models.Data;

namespace ProductApi.Controllers.Models
{
    /// <summary>
    /// Product Request
    /// </summary>
    public class CreateProductRequest
    {
        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Price with currency
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// The time until which the product remains of high quality
        /// </summary>
        public DateTimeOffset ExpirationDate { get; set; }
    }
}