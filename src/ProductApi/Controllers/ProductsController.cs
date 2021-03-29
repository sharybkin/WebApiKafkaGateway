using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands.RequestCommands;
using Common.Models.Data;
using Common.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Controllers.Models;
using ProductApi.GatewayService;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves list of products
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }


        /// <summary>
        /// Retrieves Product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(Guid productId, CancellationToken token)
        {
            var query = new GetProductByIdQuery(productId);
            var result = await _mediator.Send(query, token);
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }
        
        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var command = new CreateProductRequestCommand(request.Name, request.Price, request.ExpirationDate);
            var result = await _mediator.Send(command);
            return CreatedAtAction("GetProduct", new {productId = result.Id, token = CancellationToken.None}, result);
        }

    }
}