using System.Collections.Generic;
using Common.Commands;
using Common.Models.Data;
using MediatR;

namespace Common.Queries
{
    public class GetAllProductsQuery : Command, IRequest<List<Product>>
    {

    }
}