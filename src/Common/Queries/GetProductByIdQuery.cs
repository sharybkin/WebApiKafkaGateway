using System;
using Common.Commands;
using Common.Models.Data;
using MediatR;

namespace Common.Queries
{
    public class GetProductByIdQuery : Command, IRequest<Product>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}