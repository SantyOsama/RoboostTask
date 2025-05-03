using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Stocks;
using RoboostTask.Features.Transaction.Queries;
using RoboostTask.GeneralResponse;
using System.Security.Claims;
using RoboostTask.Features.Transactions.Commands;
using RoboostTask.Features.Stocks.Orchestrators;

namespace RoboostTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost("add-stock")]
        public async Task<Response<string>> AddStock([FromBody] AddStockRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>.Fail("Invalid input");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Response<string>.Fail("User is not authenticated");
            }

            var result = await _mediator.Send(new AddStockOrchestrator(request, userId));

            if (!result.IsSucceeded)
            {
                return Response<string>.Fail(result.Message, null, result.StatusCode);
            }

            return Response<string>.Success("Stock added successfully");
        }

        [HttpDelete("delete-stock")]
        public async Task<ActionResult<Response<string>>> DeleteStock([FromBody] DeleteStockRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Response<string>.Fail("Invalid input"));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(Response<string>.Fail("User is not authenticated"));
            }

            var result = await _mediator.Send(new DeleteStockOrchestrator(request, userId));

            if (!result.IsSucceeded)
            {
                return StatusCode(result.StatusCode, Response<string>.Fail(result.Message));
            }
            return Ok(Response<string>.Success(result.Message));
        }
        [Authorize]
        [HttpPost("transfer-stock")]
        public async Task<ActionResult<Response<bool>>> TransferStock([FromBody] TransferStockRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Response<bool>.Fail("Invalid input"));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(Response<bool>.Fail("User is not authenticated"));
            }

            var command = new TransferStockOrchestrator(request, userId);
            var result = await _mediator.Send(command);

            if (!result.IsSucceeded)
            {
                return StatusCode(result.StatusCode, Response<bool>.Fail(result.Message));
            }

            return Ok(Response<bool>.Success(true, result.Message));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("available-stocks")]
        public async Task<IActionResult> GetAvailableInventory()
        {
            var result = await _mediator.Send(new GetInventoryQuery());
            return Ok(result);
        }


        [Authorize]
        [HttpGet("check-role")]
        public IActionResult CheckRole()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = User.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();

            return Ok(new
            {
                userId = userId,
                roles = roles,
                allClaims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

    }
}
