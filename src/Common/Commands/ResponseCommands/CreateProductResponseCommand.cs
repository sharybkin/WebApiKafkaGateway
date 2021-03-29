using System;
using Common.Models.Data;
using MediatR;

namespace Common.Commands.ResponseCommands
{
    public class CreateProductResponseCommand : Command, IRequest<bool>
    {
        public CreateProductResponseCommand(Product product, Guid commandId)
        {
            Product = product;
            base.CommandId = commandId;
        }

        public Product Product { get; set; }
    }
}