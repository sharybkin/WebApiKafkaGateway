using System;
using Common.Models.Data;
using MediatR;

namespace Common.Commands.ResponseCommands
{
    public class GetProductByIdResponseCommand : Command, IRequest<bool>
    {
        public GetProductByIdResponseCommand(Product product, Guid commandId)
        {
            Product = product;
            base.CommandId = commandId;
        }

        public Product Product { get; set; }
    }
}