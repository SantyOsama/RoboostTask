using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.Features.Products.Commands;
using RoboostTask.Features.Products.Queries;

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
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = productId.Data }, productId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetProductByIdQuery(id));
            if (response.Succeeded)
                return Ok(response);
            else
                return NotFound(response);
        }
    }
}
