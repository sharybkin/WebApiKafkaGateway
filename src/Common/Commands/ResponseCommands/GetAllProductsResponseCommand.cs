using System;
using System.Collections.Generic;
using Common.Models.Data;
using MediatR;

namespace Common.Commands.ResponseCommands
{
    public class GetAllProductsResponseCommand : Command, IRequest<bool>
    {
        public GetAllProductsResponseCommand(IList<Product> products, Guid commandId)
        {
            Products = products;
            base.CommandId = commandId;
        }

        public IList<Product> Products { get; }
    }
}