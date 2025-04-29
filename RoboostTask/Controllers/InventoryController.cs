using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.Features.Inventory.Queries;

namespace RoboostTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentInventory()
        {
            var result = await _mediator.Send(new GetInventoryQuery());
            return Ok(result);
        }
    }
}
