using System;
using Common.Models.Data;
using MediatR;

namespace Common.Commands.RequestCommands
{
    public class CreateProductRequestCommand : Command, IRequest<Product>
    {
        public CreateProductRequestCommand(string name, Price price, DateTimeOffset expirationDate)
        {
            Name = name;
            Price = price;
            ExpirationDate = expirationDate;
        }

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