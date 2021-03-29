using System;

namespace Common.Models.Data
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identity
        /// </summary>
        public Guid Id { get; set; }

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