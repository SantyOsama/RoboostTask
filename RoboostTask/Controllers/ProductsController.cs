using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Products;
using RoboostTask.Features.Products.Commands;
using RoboostTask.Features.Products.Queries;
using RoboostTask.GeneralResponse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoboostTask.Controllers
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

        [HttpPost]
        public async Task<Response<string>> AddProduct([FromBody] AddProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new Response<string>().Fail(ModelState.ToString());
            }

            var result = await _mediator.Send(new AddProductCommand(request));

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Response<GetProductResponse>> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return result;
        }

        [HttpGet]
        public async Task<Response<List<GetProductResponse>>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return result;
        }

        [HttpPut("{id:int}")]
        public async Task<Response<string>> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new Response<string>().Fail(null, "Invalid model state");
            }

            var command = new UpdateProductCommand(
                Id: id,
                Name: request.Name,
                Description: request.Description,
                Price: request.Price,
                Quantity: request.Quantity,
                LowStockThreshold: request.LowStockThreshold
            );

            var result = await _mediator.Send(command);
            return result;
        }
        [HttpDelete("{id:int}")]
        public async Task<Response<string>> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return result;
        }
    }
}