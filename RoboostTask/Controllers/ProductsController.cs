using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Products;
using RoboostTask.Features.Products.Commands;
using RoboostTask.Features.Products.Orchestrators;
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
        public async Task<Response<Guid>> AddProduct([FromBody] AddProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Response<Guid>.Fail(ModelState.ToString());
            }

            var result = await _mediator.Send(new AddProductOrchestrator(request));

            return result;
        }

        [HttpGet("{id:guid}")]
        public async Task<Response<GetProductResponse>> GetProductById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdOrchestrator(id));
            return result;
        }

        [HttpGet]
        public async Task<Response<List<GetProductResponse>>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsOrchestrator());
            return result;
        }

        [HttpPut("{id:guid}")]
        public async Task<Response<string>> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return  Response<string>.Fail(ModelState.ToString());
            }

            var result = await _mediator.Send(new UpdateProductOrchestrator(request));
            return result;
        }
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<Response<bool>> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductOrchestrator(id));
            return result;
        }
    }
}